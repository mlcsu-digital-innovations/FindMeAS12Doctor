import { ApiService } from '../api/api.service';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { UNAVAILABLE } from 'src/app/constants/app.constants';
import { UserAvailability } from 'src/app/interfaces/user-availability.interface';
import { UserAvailabilityPost } from 'src/app/interfaces/availability-post.interface';
import { UserAvailabilityPut } from 'src/app/interfaces/availability-put.interface';


@Injectable({
  providedIn: 'root'
})
export class UserAvailabilityService {

  constructor(
    private apiService: ApiService
  ) { }

  public getListForUser() {
    return (this.apiService.get(
      `${environment.apiEndpoint}/useravailabilities`,
      null
    ));
  }

  private getPostType(userAvailability: UserAvailability): string {

    let postType: string;

    if (userAvailability.location.latitude !== undefined && userAvailability.location.longitude !== undefined) {
      postType = 'location';
    }

    if (userAvailability.location.postcode !== undefined) {
      postType = 'postcode';
    }

    if (userAvailability.location.contactDetailId !== undefined) {
      postType = 'contactDetail';
    }

    if (userAvailability.statusId === UNAVAILABLE) {
      postType = 'unavailable';
    }

    return postType;
  }


  public postUserAvailability(userAvailability: UserAvailability) {

    const availability = {} as UserAvailabilityPost;

    availability.start = userAvailability.start;
    availability.end = userAvailability.end;
    availability.contactDetailId = userAvailability.location.contactDetailId;
    availability.postcode = userAvailability.location.postcode;
    availability.latitude = userAvailability.location.latitude;
    availability.longitude = userAvailability.location.longitude;
    availability.statusId = userAvailability.statusId;

    const postType = this.getPostType(userAvailability);

    return (this.apiService.post(
      `${environment.apiEndpoint}/useravailabilities/${postType}`,
      availability
    ));
  }

  public putUserAvailability(userAvailability: UserAvailability) {

    const availability = {} as UserAvailabilityPut;

    availability.start = userAvailability.start;
    availability.end = userAvailability.end;
    availability.contactDetailId = userAvailability.location.contactDetailId;
    availability.postcode = userAvailability.location.postcode;
    availability.latitude = userAvailability.location.latitude;
    availability.longitude = userAvailability.location.longitude;
    availability.id = userAvailability.id;
    availability.statusId = userAvailability.statusId;

    const postType = this.getPostType(userAvailability);

    return (this.apiService.put(
      `${environment.apiEndpoint}/useravailabilities/${availability.id}/${postType}`,
      availability
    ));
  }

}
