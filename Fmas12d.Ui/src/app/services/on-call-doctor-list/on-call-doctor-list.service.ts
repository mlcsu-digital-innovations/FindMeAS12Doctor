import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { OnCallDoctorList } from 'src/app/interfaces/on-call-doctor-list';

@Injectable({
  providedIn: 'root'
})
export class OnCallDoctorListService {

  private _onCallDoctorList$: OnCallDoctorList[];

  constructor() { 
    this._onCallDoctorList$ = [];
  }

  get onCallDoctorList$(): Observable<OnCallDoctorList[]> { return of(this._onCallDoctorList$); }  
}
