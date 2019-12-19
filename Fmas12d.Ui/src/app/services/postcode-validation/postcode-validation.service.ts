import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
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

  public searchPostcode(postcode: string): Observable<any> {

    // format the supplied postcode so that it contains a space
    postcode = postcode.trim();
    if (postcode.indexOf(' ') === -1 && postcode.length > 3) {
      const inwardCode = postcode.substr(postcode.length - 3, 3);
      const outwardCode = postcode.substr(0, postcode.length - 3);
      postcode = `${outwardCode} ${inwardCode}`;
    }

    const params = new HttpParams().set('postcode', postcode);

    return this.httpClient.get(
      `${environment.apiEndpoint}/locationdetails/search`, {params}
    );
  }
}
