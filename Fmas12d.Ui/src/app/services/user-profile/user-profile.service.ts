import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { UserProfile } from 'src/app/interfaces/user-profile';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {

  constructor() { }

  GetUser(): Observable<any> {
    let user: UserProfile = {
      contactDetailTypeId: 1,
      displayName: 'John Smith',
      emailAddress: 'jsmith@nhs.net',
      genderTypeId: 2,
      gmcNumber: 1234567,
      id: 555,
      mobileNumber: '07847 435822',
      organisationName: 'Midlands and Lancashire CSU',
      profileTypeId: 2,
      section12ApprovalStatusId: 1,
      section12ExpiryDate: new Date(),
      telephoneNumber: '01256 435822',
      userSpecialities: [
        {
          id: 11,
          userId: 555,
          specialityId: 2
        },
        {
          id: 12,
          userId: 555,
          specialityId: 3
        },
      ],
      vsrNumber: 'ABC1234245'
    };

    return of(user);
  }
}
