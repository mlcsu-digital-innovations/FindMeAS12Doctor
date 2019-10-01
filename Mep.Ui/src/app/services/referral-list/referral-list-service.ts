import { Injectable, PipeTransform } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, Subject, of, throwError } from 'rxjs';
import { DecimalPipe } from '@angular/common';
import { debounceTime, switchMap, tap } from 'rxjs/operators';
import { ReferralList } from '../../interfaces/referral-list';
import { SortDirection } from '../../directives/table-header-sortable/table-header-sortable.directive';

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
}

function compare(value1: any, value2: any) {
  return value1 < value2 ? -1 : value1 > value2 ? 1 : 0;
}

function sort(referralList: ReferralList[], column: string, direction: string): ReferralList[] {
  if (direction === '') {
    return referralList;
  } else {
    return [...referralList].sort((a, b) => {
      const res = compare(a[column], b[column]);
      return direction === 'asc' ? res : -res;
    });
  }
}

function matches(referral: ReferralList, term: string, pipe: PipeTransform) {
  return pipe.transform(referral.referralId).includes(term)
    || (referral.patientIdentifier &&
        referral.patientIdentifier.toLowerCase().includes(term.toLowerCase()))
    || (referral.leadAmhp &&
        referral.leadAmhp.toLowerCase().includes(term.toLowerCase()))
    || (referral.numberOfExaminationAttempts &&
        pipe.transform(referral.numberOfExaminationAttempts).includes(term))
    || (referral.speciality &&
        referral.speciality.toLowerCase().includes(term.toLowerCase()))
    || (referral.timescale &&
        pipe.transform(referral.timescale).includes(term.toLowerCase()))
    || (referral.status &&
        referral.status.toLowerCase().includes(term.toLowerCase()))
    || (referral.responsesReceived &&
        pipe.transform(referral.responsesReceived).includes(term.toLowerCase()))
    || (referral.doctorsAllocated &&
        pipe.transform(referral.doctorsAllocated).includes(term.toLowerCase()));
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
    sortDirection: ''
  };

  constructor(
    private pipe: DecimalPipe,
    private http: HttpClient) {

    let configUrl = 'https://localhost:5001/api/referral/list';

    this._search$.pipe(
      tap(() => this._loading$.next(true)),
      debounceTime(200),
      switchMap(() => this._search()),
      tap(() => this._loading$.next(false))
    ).subscribe(result => {
      this._referralList$.next(result.referralList);
      this._total$.next(result.total);
    });

    this.http.get<ReferralList[]>(configUrl).subscribe(
      (data: ReferralList[]) => {
        this._rawReferralList = data;
        this._search$.next();
      },
      (error: any) => {
        this._loading$.next(false);
        this._referralList$.error(
          "Server Error: Unable to obtain referral list! " +
          "Please try again in a few moments");        
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

  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this._search$.next();
  }

  private _search(): Observable<SearchResult> {
    const { sortColumn, sortDirection, pageSize, page, searchTerm } = this._state;

    // 1. sort
    let referralList = sort(this._rawReferralList, sortColumn, sortDirection);

    // 2. filter
    referralList = referralList.filter(referral => matches(referral, searchTerm, this.pipe));
    const total = referralList.length;

    // 3. paginate
    referralList = referralList.slice((page - 1) * pageSize, (page - 1) * pageSize + pageSize);
    return of({ referralList: referralList, total });
  }
}