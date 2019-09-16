import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostcodeValidationService {
  constructor(private httpClient: HttpClient) {}

  public validatePostcode(postcode: string) {
    // const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.get(
      `${environment.apiEndpoint}/locationdetails/${postcode}`
    );
  }
}
