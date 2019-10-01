import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PostcodeValidationService {
  constructor(private httpClient: HttpClient) {}

  public validatePostcode(postcode: string) {
    return this.httpClient.get(
      `${environment.apiEndpoint}/locationdetails/${postcode}`
    );
  }
}
