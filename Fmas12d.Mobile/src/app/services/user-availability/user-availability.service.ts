import { ApiService } from '../api/api.service';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { UserAvailability } from 'src/app/interfaces/user-availability.interface';


@Injectable({
  providedIn: 'root'
})
export class UserAvailabilityService {

  constructor(
    private apiService: ApiService
  ) { }

  public getPostcodeDetails(postcode: string) {
    return (this.apiService.get(
      `${environment.apiEndpoint}/locationdetails/${postcode}`,
      null
    ));
  }

  public postUserAvailability(userAvailability: UserAvailability) {

    let postType: string;

    if (userAvailability.contactDetailId !== undefined) {
      postType = 'contactDetail';
    }

    if (userAvailability.postcode !== undefined) {
      postType = 'postcode';
    }

    if (userAvailability.latitude !== undefined && userAvailability.longitude !== undefined) {
      postType = 'location';
    }

    if (userAvailability.isAvailable === false) {
      postType = 'unavailable';
    }

    return (this.apiService.post(
      `${environment.apiEndpoint}/useravailabilities/${postType}`,
      userAvailability
    ));
  }

}
