import { Injectable, PipeTransform } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, Subject, of, throwError } from 'rxjs';
import { DecimalPipe } from '@angular/common';
import { debounceTime, switchMap, tap } from 'rxjs/operators';
import { ReferralList } from '../../interfaces/referral-list';
import { SortDirection } from '../../directives/table-header-sortable/table-header-sortable.directive';
import { environment } from 'src/environments/environment';
import * as moment from 'moment';

interface SearchResult {
  referralList: ReferralList[];
  total: number;
}

interface State {
  page: number;
  pageSize: number;
  searchTerm: string;
  sortColumn: string;
  sortDirection: SortDirection;
  sortColumnType: string;
}

function compare(value1: any, value2: any, sortColumnType: string) {

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

function sort(referralList: ReferralList[], column: string, direction: string, sortColumnType: string): ReferralList[] {
  if (direction === '') {
    return referralList;
  } else {
    return [...referralList].sort((a, b) => {
      const res = compare(a[column], b[column], sortColumnType);
      return direction === 'asc' ? res : -res;
    });
  }
}

function matches(referral: ReferralList, term: string, pipe: PipeTransform) {
  let termLowerCase = term.toLowerCase();
  return pipe.transform(referral.referralId).includes(termLowerCase)
    || (referral.patientIdentifier &&
      referral.patientIdentifier.toLowerCase().includes(termLowerCase))
    || (referral.leadAmhp &&
      referral.leadAmhp.toLowerCase().includes(termLowerCase))
    || (referral.numberOfAssessmentAttempts &&
      pipe.transform(referral.numberOfAssessmentAttempts).includes(termLowerCase))
    || (referral.assessmentLocationPostcode &&
      referral.assessmentLocationPostcode.toLowerCase().includes(termLowerCase))
    || (referral.specialityName &&
      referral.specialityName.toLowerCase().includes(termLowerCase))
    || (referral.timescale &&
      referral.timescale.toString().toLowerCase().includes(termLowerCase))
    || (referral.statusName &&
      referral.statusName.toLowerCase().includes(termLowerCase))
    || (referral.responsesReceived &&
      pipe.transform(referral.responsesReceived).includes(termLowerCase))
    || (referral.doctorsAllocated &&
      pipe.transform(referral.doctorsAllocated).includes(termLowerCase));
}

@Injectable()
export class ReferralListService {

  private _loading$ = new BehaviorSubject<boolean>(true);
  private _rawReferralList: ReferralList[];
  private _referralList$ = new BehaviorSubject<ReferralList[]>([]);
  private _search$ = new Subject<void>();
  private _total$ = new BehaviorSubject<number>(0);
  private _error$ = new Observable();

  private _state: State = {
    page: 1,
    pageSize: 10,
    searchTerm: '',
    sortColumn: '',
    sortDirection: '',
    sortColumnType: ''
  };

  constructor(
    private pipe: DecimalPipe,
    private http: HttpClient) {

    this._search$.pipe(
      tap(() => this._loading$.next(true)),
      debounceTime(200),
      switchMap(() => this._search()),
      tap(() => this._loading$.next(false))
    ).subscribe(result => {
      this._referralList$.next(result.referralList);
      this._total$.next(result.total);
    });


    let endpoint = environment.apiEndpoint + '/referral/list/open';
    this.http.get<ReferralList[]>(endpoint).subscribe(
      (data: ReferralList[]) => {

        if (data !== null) {
          data.forEach(item => {
            item.doctorsSelectedAllocatedAttended = 
              `${item.doctorsSelected} / ${item.doctorsAllocated} / ${item.doctorsAttended}`;
            item.responsesReceivedAccepted = 
              `${item.responsesReceived} / ${item.responsesAccepted}`;
          });
        }

        this._rawReferralList = data;
        this._search$.next();
      },
      (error: any) => {
        this._loading$.next(false);
        this._referralList$.error(
          'Server Error: Unable to obtain referral list! ' +
          'Please try again in a few moments');
      }
    );
  }

  get loading$() { return this._loading$.asObservable(); }
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }
  get referralList$() { return this._referralList$.asObservable(); }
  get searchTerm() { return this._state.searchTerm; }
  get total$() { return this._total$.asObservable(); }

  set page(page: number) { this._set({ page }); }
  set pageSize(pageSize: number) { this._set({ pageSize }); }
  set searchTerm(searchTerm: string) { this._set({ searchTerm }); }
  set sortColumn(sortColumn: string) { this._set({ sortColumn }); }
  set sortDirection(sortDirection: SortDirection) { this._set({ sortDirection }); }
  set sortColumnType(sortColumnType: string) { this._set({ sortColumnType }); }

  private _set(patch: Partial<State>) {
    if (this._rawReferralList) {
      Object.assign(this._state, patch);
      this._search$.next();
    }
  }

  private _search(): Observable<SearchResult> {
    const { sortColumn, sortDirection, sortColumnType, pageSize, page, searchTerm } = this._state;

    if (this._rawReferralList != null) {
      // 1. sort
      let referralList = sort(this._rawReferralList, sortColumn, sortDirection, sortColumnType) ;

      // 2. filter
      referralList = referralList.filter(referral => matches(referral, searchTerm, this.pipe));
      const total = referralList.length;

      // 3. paginate
      referralList = referralList.slice((page - 1) * pageSize, (page - 1) * pageSize + pageSize);

      return of({ referralList: referralList, total });

    } else {

      return of({ referralList: [], total: 0 });
    }
  }
}
