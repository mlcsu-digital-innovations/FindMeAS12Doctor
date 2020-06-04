import { DecimalPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, BehaviorSubject, Subject } from 'rxjs';
import { OnCallDoctorList } from 'src/app/interfaces/on-call-doctor-list';
import { SortDirection } 
  from 'src/app/directives/table-header-sortable/table-header-sortable.directive';
import { environment } from 'src/environments/environment';
import { tap, debounceTime, switchMap } from 'rxjs/operators';
import * as moment from 'moment';

interface SearchResult {
  onCallDoctorList: OnCallDoctorList[];
  total: number;
}

interface State {
  page: number;
  pageSize: number;
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
      returnValue = moment(value1).isBefore(moment(value2)) ? -1 : 
        moment(value1).isAfter(moment(value2)) ? 1 : 0;
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

function sort(
  referralList: OnCallDoctorList[], 
  column: string, 
  direction: string, 
  sortColumnType: string): OnCallDoctorList[] 
{
  if (direction === '') {
    return referralList;
  } else {
    return [...referralList].sort((a, b) => {
      const res = compare(a[column], b[column], sortColumnType);
      return direction === 'asc' ? res : -res;
    });
  }
}

@Injectable({
  providedIn: 'root'
})
export class OnCallDoctorListService {

  private _loading$ = new BehaviorSubject<boolean>(true);
  private _rawOnCallDoctorList: OnCallDoctorList[];
  private _onCallDoctorList$: BehaviorSubject<OnCallDoctorList[]> 
    = new BehaviorSubject<OnCallDoctorList[]>([]);
  private _sort$ = new Subject<void>();
  private _total$ = new BehaviorSubject<number>(0);

  private _state: State = {
    page: 1,
    pageSize: 10,
    sortColumn: '',
    sortDirection: '',
    sortColumnType: ''
  };

  constructor(private pipe: DecimalPipe, private http: HttpClient) { 
    this.InitialiseService();
  }

  public InitialiseService() {
    this._sort$.pipe(
      tap(() => this._loading$.next(true)),
      debounceTime(200),
      switchMap(() => this._sort()),
      tap(() => this._loading$.next(false))
    ).subscribe(result => {
      this._onCallDoctorList$.next(result.onCallDoctorList);
      this._total$.next(result.total);
    });

    let endpoint = environment.apiEndpoint + '/user/oncall';
    this.http.get<OnCallDoctorList[]>(endpoint).subscribe(
      (data: OnCallDoctorList[]) => {
        this._rawOnCallDoctorList = data;
        this._sort$.next();
      },
      (error: any) => {
        this._loading$.next(false);
        this._onCallDoctorList$.error(
          'Server Error: Unable to obtain On Call doctor list! ' +
          'Please try again in a few moments');
      }
    );  
  }

  get loading$() { return this._loading$.asObservable(); }
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }
  get onCallDoctorList$(): Observable<OnCallDoctorList[]> 
  { 
    return this._onCallDoctorList$.asObservable();
  }  
  get total$() { return this._total$.asObservable(); }

  set page(page: number) { this._set({ page }); }
  set pageSize(pageSize: number) { this._set({ pageSize }); }
  set sortColumn(sortColumn: string) { this._set({ sortColumn }); }
  set sortDirection(sortDirection: SortDirection) { this._set({ sortDirection }); }
  set sortColumnType(sortColumnType: string) { this._set({ sortColumnType }); }  

  availabilitiesForDoctor(userId: number, start: Date, end: Date): OnCallDoctorList[] {
   return this._rawOnCallDoctorList.filter((onCallDoctorListItem: OnCallDoctorList) => 
      userId == onCallDoctorListItem.userId &&
      (
        (start >= new Date(onCallDoctorListItem.start) && start <= new Date(onCallDoctorListItem.end)) || 
        (end >= new Date(onCallDoctorListItem.start) && end <= new Date(onCallDoctorListItem.end))
      )
    );
  }

  private _set(patch: Partial<State>) {
    if (this._rawOnCallDoctorList) {
      Object.assign(this._state, patch);
      this._sort$.next();
    }
  }

  private _sort(): Observable<SearchResult> {
    const { sortColumn, sortDirection, sortColumnType, pageSize, page } = this._state;

    if (this._rawOnCallDoctorList != null) {
      let onCallDoctorList 
        = sort(this._rawOnCallDoctorList, sortColumn, sortDirection, sortColumnType);
      const total = onCallDoctorList.length;
      onCallDoctorList 
        = onCallDoctorList.slice((page - 1) * pageSize, (page - 1) * pageSize + pageSize);

      return of({ onCallDoctorList: onCallDoctorList, total });

    } else {
      return of({ onCallDoctorList: [], total: 0 });
    }
  }
}
