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
import { FilterItem } from 'src/app/interfaces/filterItem';

@Injectable({
  providedIn: 'root'
})
export class FinanceClaimListService {

  _claims$ = new BehaviorSubject<FinanceClaim[]>([]);
  _loading$ = new BehaviorSubject<boolean>(true);
  _search$ = new Subject<void>();
  _total$ = new BehaviorSubject<number>(0);
  rawClaimsList: FinanceClaim[] = [];

  _statusFilter: FilterItem[];
  _ccgFilter: FilterItem[];
  _claimantFilter: FilterItem[];
  _exportedFilter: FilterItem[];

  private _filteringOnStatus: boolean;
  private _filteringOnCcg: boolean;
  private _filteringOnClaimant: boolean;
  private _filteringOnExported: boolean;

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

  matches(claim: FinanceClaim, term: string, pipe: PipeTransform) {
    return pipe.transform(claim.claimReference).includes(term.toLowerCase())
      || claim.claimant.displayName.toLowerCase().includes(term.toLowerCase())
      || claim.ccg.name.toLowerCase().includes(term.toLowerCase())
      || claim.claimStatus.name.toLowerCase().includes(term.toLowerCase());
  }

  sort(claims: FinanceClaim[], column: string, direction: string, columnType: string): FinanceClaim[] {
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
      this._claims$.next(result.claims as FinanceClaim[]);
      this._total$.next(result.total);
    });
  }

  getClaims(refresh: boolean = false) {
    if (refresh || !this._claims$) {
      this.httpClient.get(
        `${environment.apiEndpoint}/financeassessmentclaim/list`
      ).subscribe((result: FinanceClaim[]) => {
        this.rawClaimsList = result;

        this._statusFilter =
        result.map((x: FinanceClaim) => (
          {id: x.claimStatus.id, name: x.claimStatus.name, selected: false})
        )
        .sort((a, b) => a.name.localeCompare(b.name))
        .filter((status, i, arr) => arr.findIndex(t => t.id === status.id) === i);

        this._ccgFilter =
        result.map((x: FinanceClaim) => ({id: x.ccg.id, name: x.ccg.name, selected: false}))
        .sort((a, b) => a.name.localeCompare(b.name))
        .filter((status, i, arr) => arr.findIndex(t => t.id === status.id) === i);

        this._claimantFilter =
        result.map((x: FinanceClaim) => (
          {id: x.claimant.id, name: x.claimant.displayName, selected: false})
        )
        .sort((a, b) => a.name.localeCompare(b.name))
        .filter((status, i, arr) => arr.findIndex(t => t.id === status.id) === i);

        this._exportedFilter = [];
        this._exportedFilter.push({id: 1, name: 'Yes', selected: false});
        this._exportedFilter.push({id: 2, name: 'No', selected: true});

        this._search$.next();
      });

    }
    return this._claims$.asObservable();
  }

  clearFilter(list: FilterItem[]) {
    list.forEach(item => {
      item.selected = false;
    });
    this._search$.next();
  }

  filterChanged() {
    this._search$.next();
  }

  get claims$() { return this._claims$.asObservable(); }
  get total$() { return this._total$.asObservable(); }
  get loading$() { return this._loading$.asObservable(); }
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }
  get searchTerm() { return this._state.searchTerm; }

  get activeStatuses() { return this._statusFilter; }
  get activeCcgs() { return this._ccgFilter; }
  get activeClaimants() { return this._claimantFilter; }
  get activeExported() { return this._exportedFilter; }

  get filteringOnStatus() { return this._filteringOnStatus; }
  get filteringOnCcg() { return this._filteringOnCcg; }
  get filteringOnClaimant() { return this._filteringOnClaimant; }
  get filteringOnExported() { return this._filteringOnExported; }

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

    // turn off filter indicators
    this._filteringOnStatus = false;
    this._filteringOnCcg = false;
    this._filteringOnClaimant = false;
    this._filteringOnExported = false;

    // 1. sort
    let claims = this.sort(this.rawClaimsList, sortColumn, sortDirection, sortColumnType);

    // 2. filter by keyword
    claims = claims.filter(claim => this.matches(claim, searchTerm, this.pipe));

    // 3. filter by items

    const activeStatusFilters = this._statusFilter
      .filter(status => status.selected === true)
      .map(status => status.id);
    if (activeStatusFilters.length > 0) {
      claims = claims.filter(claim => activeStatusFilters.includes(claim.claimStatus.id));
      this._filteringOnStatus = true;
    }

    const activeCcgFilters = this._ccgFilter
      .filter(status => status.selected === true)
      .map(status => status.id);
    if (activeCcgFilters.length > 0) {
      claims = claims.filter(claim => activeCcgFilters.includes(claim.ccg.id));
      this._filteringOnCcg = true;
    }

    const activeClaimantFilters = this._claimantFilter
      .filter(status => status.selected === true)
      .map(status => status.id);
    if (activeClaimantFilters.length > 0) {
      claims = claims.filter(claim => activeClaimantFilters.includes(claim.claimant.id));
      this._filteringOnClaimant = true;
    }

    const activeExportedFilters = this._exportedFilter
      .filter(status => status.selected === true)
      .map(status => status.id);
    if (activeExportedFilters.length === 1) {
      if (activeExportedFilters.includes(1)) {
        claims = claims.filter(claim => claim.exportedDate !== null);
      } else {
        claims = claims.filter(claim => claim.exportedDate === null);
      }
      this._filteringOnExported = true;
    }

    const total = claims.length;

    // 4. paginate
    claims = claims.slice((page - 1) * pageSize, (page - 1) * pageSize + pageSize);
    return of({claims, total});
  }
}
