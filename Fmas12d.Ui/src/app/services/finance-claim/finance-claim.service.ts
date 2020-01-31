import { BehaviorSubject, Subject, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FinanceClaim } from 'src/app/interfaces/finance-claim';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable, PipeTransform } from '@angular/core';
import { map, tap, debounceTime, switchMap, delay } from 'rxjs/operators';
import { State } from 'src/app/interfaces/state';
import { DecimalPipe } from '@angular/common';
import { ClaimSearchResult } from 'src/app/interfaces/claim-search-result';
import { SortDirection } from 'src/app/directives/table-header-sortable/table-header-sortable.directive';


@Injectable({
  providedIn: 'root'
})
export class FinanceClaimService {

  _loading$ = new BehaviorSubject<boolean>(true);
  _search$ = new Subject<void>();
  _claims$ = new BehaviorSubject<FinanceClaim[]>([]);
  _total$ = new BehaviorSubject<number>(0);
  rawClaimsList: FinanceClaim[] = [];

  private _state: State = {
    page: 1,
    pageSize: 10,
    searchTerm: '',
    sortColumn: '',
    sortDirection: ''
  };

  compare(v1, v2) {
    return v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
  }

  matches(claim: FinanceClaim, term: string, pipe: PipeTransform) {
    return pipe.transform(claim.claimReference).includes(term.toLowerCase())
      || claim.claimant.displayName.toLowerCase().includes(term)
      || claim.ccg.name.toLowerCase().includes(term)
      || claim.claimStatus.name.toLowerCase().includes(term);
  }

  sort(claims: FinanceClaim[], column: string, direction: string): FinanceClaim[] {
    if (direction === '') {
      return claims;
    } else {
      return [...claims].sort((a, b) => {
        const res = this.compare(a[column], b[column]);
        return direction === 'asc' ? res : -res;
      });
    }
  }

  constructor(private httpClient: HttpClient, private pipe: DecimalPipe) {
    this._search$.pipe(
      tap(() => this._loading$.next(true)),
      debounceTime(200),
      switchMap(() => this._search()),
      delay(200),
      tap(() => this._loading$.next(false))
    ).subscribe(result => {
      this._claims$.next(result.claims);
      this._total$.next(result.total);
    });

    this.httpClient.get(
      `${environment.apiEndpoint}/financeassessmentclaim/list`
    ).subscribe((result: FinanceClaim[]) => {
      this.rawClaimsList = result;
      this._search$.next();
    });
  }

  get claims$() { return this._claims$.asObservable(); }
  get total$() { return this._total$.asObservable(); }
  get loading$() { return this._loading$.asObservable(); }
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }
  get searchTerm() { return this._state.searchTerm; }

  set page(page: number) { this._set({page}); }
  set pageSize(pageSize: number) { this._set({pageSize}); }
  set searchTerm(searchTerm: string) { this._set({searchTerm}); }
  set sortColumn(sortColumn: string) { this._set({sortColumn}); }
  set sortDirection(sortDirection: SortDirection) { this._set({sortDirection}); }

  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this._search$.next();
  }

  private _search(): Observable<ClaimSearchResult> {
    const {sortColumn, sortDirection, pageSize, page, searchTerm} = this._state;

    // 1. sort
    let claims = this.sort(this.rawClaimsList, sortColumn, sortDirection);

    // 2. filter
    claims = claims.filter(claim => this.matches(claim, searchTerm, this.pipe));
    const total = claims.length;

    // 3. paginate
    claims = claims.slice((page - 1) * pageSize, (page - 1) * pageSize + pageSize);
    return of({claims, total});
  }
}
