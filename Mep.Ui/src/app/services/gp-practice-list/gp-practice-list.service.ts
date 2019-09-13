import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';

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
