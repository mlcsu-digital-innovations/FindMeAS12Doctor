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

  public SearchUnregisteredUsers(userName?: string, gmcNumber?: number): Observable<UnregisteredUser | any> {

    console.log('searching - ' + userName);
    console.log('searching - ' + gmcNumber);

    const dummyUser1: UnregisteredUser = {
      address: 'Hucknall Road, Nottingham',
      displayName: 'Bob',
      organisation: 'Nottingham University Hospitals NHS Trust',
      gender: 'Male',
      genderId: 2,
      gmcNumber: 1234567,
      id: 1,
      isSection12: true,
      numberOfAssessments: 1,
      postcode: 'NG5 1PB',
      contact: '0121 234 5678',
      type: 'doctor'
    };

    const dummyUser2: UnregisteredUser = {
      address: 'Hucknall Road, Nottingham',
      displayName: 'Fred',
      organisation: 'Nottingham University Hospitals NHS Trust',
      gender: 'Male',
      genderId: 2,
      gmcNumber: 7654321,
      id: 1,
      isSection12: true,
      numberOfAssessments: 1,
      postcode: 'NG5 1PB',
      contact: '0121 234 5678',
      type: 'doctor'
    };

    const users: UnregisteredUser[] = [];
    users.push(dummyUser1, dummyUser2);

    const userList = from(users);

    return userList
    .pipe(delay(2000))
    .pipe(filter(user => (user.displayName.toLowerCase() === userName.toLowerCase() || user.gmcNumber === gmcNumber)))
    .pipe(take(1))
    .pipe(defaultIfEmpty(null));
  }
}
