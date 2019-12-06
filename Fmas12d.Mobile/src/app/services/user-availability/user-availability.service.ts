import { ApiService } from '../api/api.service';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
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

    if (userAvailability.location.contactDetailId !== undefined) {
      postType = 'contactDetail';
    }

    if (userAvailability.location.latitude !== undefined && userAvailability.location.longitude !== undefined) {
      postType = 'location';
    }

    if (userAvailability.location.postcode !== undefined) {
      postType = 'postcode';
    }

    if (userAvailability.isAvailable === false) {
      postType = 'unavailable';
    }

    return postType;
  }


  public postUserAvailability(userAvailability: UserAvailability) {

    const postAvailability = {} as UserAvailabilityPost;

    postAvailability.start = userAvailability.start;
    postAvailability.end = userAvailability.end;
    postAvailability.isAvailable = userAvailability.isAvailable;
    postAvailability.contactDetailId = userAvailability.location.contactDetailId;
    postAvailability.postcode = userAvailability.location.postcode;
    postAvailability.latitude = userAvailability.location.latitude;
    postAvailability.longitude = userAvailability.location.longitude;

    const postType = this.getPostType(userAvailability);

    return (this.apiService.post(
      `${environment.apiEndpoint}/useravailabilities/${postType}`,
      postAvailability
    ));
  }

  public putUserAvailability(userAvailability: UserAvailability) {

    console.log(userAvailability);

    const putAvailability = {} as UserAvailabilityPut;

    putAvailability.start = userAvailability.start;
    putAvailability.end = userAvailability.end;
    putAvailability.isAvailable = userAvailability.isAvailable;
    putAvailability.contactDetailId = userAvailability.location.contactDetailId;
    putAvailability.postcode = userAvailability.location.postcode;
    putAvailability.latitude = userAvailability.location.latitude;
    putAvailability.longitude = userAvailability.location.longitude;
    putAvailability.id = userAvailability.id;

    const postType = this.getPostType(userAvailability);

    return (this.apiService.put(
      `${environment.apiEndpoint}/useravailabilities/${putAvailability.id}/${postType}`,
      putAvailability
    ));
  }

}
