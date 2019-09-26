import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GpPracticeListService {
  constructor(private httpClient: HttpClient) {}

  public GetGpPracticeList(searchTerm: string) {

    if (searchTerm === '' || searchTerm.length < 3) {
      return of([]);
    }
    const searchString = `searchString=${searchTerm}`;
    const headers = new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded');

    return this.httpClient.post(
      environment.apiEndpoint + '/gppracticesearch',
      searchString,
      { headers }
    ).pipe(
      map(response => response)
    );
  }
}
