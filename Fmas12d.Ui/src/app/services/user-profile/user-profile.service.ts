import { Injectable } from '@angular/core';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { UserProfile } from 'src/app/interfaces/user-profile';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {
  private _loading$ = new BehaviorSubject<boolean>(true);
  private _user: UserProfile;

  constructor() { 
    this._user = {
      contactDetails: [
        {
          address1: '27 Church Street',
          town: 'Stoke on Trent', 
          contactDetailTypeId: 1,
          email: 'a@a.com',
          id: 51,
          latitude: 52.689081,
          longitude: -2.435844,
          mobileNumber: '07456 473922',
          name: 'Base',
          postcode: 'TF2 9EF',
          telephoneNumber: '01346 475866'
        },
        {
          address1: '53 Hassall Road',
          town: 'Stoke on Trent',
          contactDetailTypeId: 2,
          email: 'b@b.com',
          id: 52,
          latitude: 52.681412,
          longitude: -2.427571,
          mobileNumber: '07846 284965',
          name: 'Home',
          postcode: 'TF2 9YU',
          telephoneNumber: '01572 573955'
        },
      ],
      contactDetailTypeId: 1,
      displayName: 'John Smith',
      emailAddress: 'jsmith@nhs.net',
      financeDetails: [
        {
          id: 11,
          ccgId: 114,
          ccgName: 'Shropshire',
          VsrNumber: 1234567
        },
        {
          id: 12,
          ccgId: 7,
          ccgName: 'Staffordshire',
          VsrNumber: 123456789
        }      
      ],
      genderTypeId: 2,
      gmcNumber: 1234567,
      id: 555,
      isAmhp: false,
      isDoctor: true,
      isFinance: false,
      mobileNumber: '07847 435822',
      organisationName: 'Midlands and Lancashire CSU',
      profileTypeId: 3,
      section12ApprovalStatusId: 1,
      section12ExpiryDate: new Date(),
      telephoneNumber: '01256 435822',
      specialities: [
        {
          name: 'Children',
          id: 2,
          resultText: null            
        },
        {
          name: 'Learning Disability',
          id: 3,
          resultText: null    
        },
      ]
    };
  }

  get loading$() { return this._loading$.asObservable(); }

  GetUser(): Observable<UserProfile> {
    this._loading$.next(true);    
    this._loading$.next(false);
    return of(this._user);
  }

  UpdateUser(user: UserProfile): Observable<UserProfile> {
    this._user = user;
    return of(this._user);
  }

}
