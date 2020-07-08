import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, delay, filter, defaultIfEmpty, take } from 'rxjs/operators';
import { of, from, Observable } from 'rxjs';
import { UserDetails } from 'src/app/interfaces/user-details';
import { UnregisteredUser } from 'src/app/interfaces/unregistered-user';

@Injectable({
  providedIn: 'root'
})
export class UnregisteredUserService {
  constructor(private httpClient: HttpClient) {}

  public SearchUnregisteredUsers(userName?: string, gmcNumber?: number): Observable<UnregisteredUser[] | any> {

    const dummyUser1: UnregisteredUser = {
      address: 'Hucknall Road, Nottingham',
      displayName: 'Bob',
      organisation: 'Nottingham University Hospitals NHS Trust',
      fromSection12LiveRegister: false,
      gender: 'Male',
      genderId: 2,
      gmcNumber: 1234567,
      id: 1,
      isSection12: true,
      numberOfAssessments: 1,
      postcode: 'NG5 1PB',
      contact: '0191 435 8765',
      type: 'Consultant'
    };

    const dummyUser2: UnregisteredUser = {
      address: 'Martin Road, Nottingham',
      displayName: 'Fred',
      organisation: 'Other Hospital NHS Trust',
      fromSection12LiveRegister: false,
      gender: 'Female',
      genderId: 1,
      gmcNumber: 7654321,
      id: 1,
      isSection12: true,
      numberOfAssessments: 1,
      postcode: 'NG9 1DR',
      contact: '0121 234 5678',
      type: 'Consultant'
    };

    const users: UnregisteredUser[] = [];
    users.push(dummyUser1, dummyUser2);

    const userList = users.filter(user => (user.displayName.toLowerCase() === userName.toLowerCase() || user.gmcNumber === gmcNumber));

    return of(userList);

    // return userList
    // .pipe(delay(2000))
    // .pipe(filter(user => (user.displayName.toLowerCase() === userName.toLowerCase() || user.gmcNumber === gmcNumber)))
    // // .pipe(take(1))
    // .pipe(defaultIfEmpty(null));
  }
}
