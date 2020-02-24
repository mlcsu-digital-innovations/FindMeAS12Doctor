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
      contactDetails: [
        {
          address1: '876 London Road',
          town: 'Stoke on Trent', 
          contactDetailTypeId: 1,
          id: 51,
          mobileNumber: '07456 473922',
          name: 'Base',
          postcode: 'ST4 5NX',
          telephoneNumber: '01346 475866'
        },
        {
          address1: '53 Hassall Road',
          town: 'Stoke on Trent',
          contactDetailTypeId: 2,
          id: 52,
          mobileNumber: '07846 284965',
          name: 'Home',
          postcode: 'ST4 5NX',
          telephoneNumber: '01572 573955'
        },
      ],
      contactDetailTypeId: 1,
      displayName: 'John Smith',
      emailAddress: 'jsmith@nhs.net',
      genderTypeId: 2,
      gmcNumber: 1234567,
      id: 555,
      mobileNumber: '07847 435822',
      organisationName: 'Midlands and Lancashire CSU',
      profileTypeId: 3,
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
