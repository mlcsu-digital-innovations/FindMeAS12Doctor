import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, delay } from 'rxjs/operators';
import { of } from 'rxjs';
import { UserDetails } from 'src/app/interfaces/user-details';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {
  constructor(private httpClient: HttpClient) {}

  public GetUserDetails(id: number) {

    const dummyUser: UserDetails = {
      displayName: 'Bob',
      id: 1,
      type: 'doctor',
      telephone: '0121 234 5678',
      gender: 'Male',
      specialities:['Children', 'Learning Disabilities'],
      gmcNumber: 1234567
    };

    return of(dummyUser)
      .pipe(delay(2000));
  }
}
