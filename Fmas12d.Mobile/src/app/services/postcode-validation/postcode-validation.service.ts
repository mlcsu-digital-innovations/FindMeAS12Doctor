import { ApiService } from '../api/api.service';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class PostcodeValidationService {

  constructor(
    private apiService: ApiService
  ) { }

  public getPostcodeDetails(postcode: string) {
    return (this.apiService.get(
      `${environment.apiEndpoint}/locationdetails/${postcode}`,
      null
    ));
  }
}
