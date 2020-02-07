import { BehaviorSubject, Subject, Observable, of } from 'rxjs';
import { ClaimSearchResult } from 'src/app/interfaces/claim-search-result';
import { DecimalPipe } from '@angular/common';
import { environment } from 'src/environments/environment';
import { FinanceClaim } from 'src/app/interfaces/finance-claim';
import { HttpClient } from '@angular/common/http';
import { Injectable, PipeTransform } from '@angular/core';
import { SortDirection } from 'src/app/directives/table-header-sortable/table-header-sortable.directive';
import { State } from 'src/app/interfaces/state';
import { tap, debounceTime, switchMap, delay } from 'rxjs/operators';
import * as moment from 'moment';
import { UserAssessmentClaim } from 'src/app/interfaces/user-assessment-claim';

@Injectable({
  providedIn: 'root'
})
export class DoctorClaimListService {

  _claims$ = new BehaviorSubject<UserAssessmentClaim[]>([]);
  _loading$ = new BehaviorSubject<boolean>(true);
  _search$ = new Subject<void>();
  _total$ = new BehaviorSubject<number>(0);
  rawClaimsList: UserAssessmentClaim[] = [];

  private _state: State = {
    page: 1,
    pageSize: 10,
    searchTerm: '',
    sortColumn: '',
    sortColumnType: '',
    sortDirection: ''
  };

  compare(value1: any, value2: any, sortColumnType: string) {

    let returnValue = 0;

    switch (sortColumnType) {
      case 'dateTime':
        const defaultDate = new Date();
        defaultDate.setFullYear(2000);
        value1 = moment(value1 === null ? defaultDate : value1);
        value2 = moment(value2 === null ? defaultDate : value2);
        returnValue = moment(value1).isBefore(moment(value2)) ? -1 : moment(value1).isAfter(moment(value2)) ? 1 : 0;
        break;
      case 'string':
        value1 = value1 === (null || undefined) ? '' : String(value1);
        value2 = value2 === (null || undefined) ? '' : String(value2);
        returnValue = value1 < value2 ? -1 : value1 > value2 ? 1 : 0;
        break;
      case 'number':
        value1 = value1 === (null || undefined) ? 0 : +value1;
        value2 = value2 === (null || undefined) ? 0 : +value2;
        returnValue = value1 < value2 ? -1 : value1 > value2 ? 1 : 0;
        break;
    }

    return returnValue;
  }

  matches(claim: UserAssessmentClaim, term: string, pipe: PipeTransform) {
    return pipe.transform(claim.claimReference).includes(term.toLowerCase())
      || claim.assessment.postcode.toLowerCase().includes(term.toLowerCase())
      || claim.claimStatus.name.toLowerCase().includes(term.toLowerCase());
  }

  sort(claims: UserAssessmentClaim[], column: string, direction: string, columnType: string): UserAssessmentClaim[] {
    if (direction === '') {
      return claims;
    } else {
      return [...claims].sort((a, b) => {

        let res: number;

        if (column.includes('.')) {

          const childProperty = column.split('.')[0];
          const grandChildProperty = column.split('.')[1];

          const compare1 = a[childProperty][grandChildProperty];
          const compare2 = b[childProperty][grandChildProperty];

          res = this.compare(compare1, compare2, columnType);

        } else {
          res = this.compare(a[column], b[column], columnType);
        }
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
  }

  getClaims(refresh: boolean = false) {
    if (refresh || !this._claims$) {
      this.httpClient.get(
        `${environment.apiEndpoint}/assessmentclaim/doctor/list`
      ).subscribe((result: UserAssessmentClaim[]) => {
        this.rawClaimsList = result;
        this._search$.next();
      });

    }
    return this._claims$.asObservable();
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
  set sortColumnType(sortColumnType: string) { this._set({ sortColumnType }); }

  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this._search$.next();
  }

  private _search(): Observable<ClaimSearchResult> {
    const {sortColumn, sortDirection, sortColumnType, pageSize, page, searchTerm} = this._state;

    // 1. sort
    let claims = this.sort(this.rawClaimsList, sortColumn, sortDirection, sortColumnType);

    // 2. filter
    claims = claims.filter(claim => this.matches(claim, searchTerm, this.pipe));
    const total = claims.length;

    // 3. paginate
    claims = claims.slice((page - 1) * pageSize, (page - 1) * pageSize + pageSize);
    return of({claims, total});
  }
}
