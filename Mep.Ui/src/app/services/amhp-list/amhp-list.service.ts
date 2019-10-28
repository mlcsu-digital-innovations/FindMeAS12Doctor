import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AmhpListService {
  constructor(private httpClient: HttpClient) {}

  public GetAmhpList(searchTerm: string) {

    if (searchTerm === '' || searchTerm.length < 3) {
      return of([]);
    }
    const options = { params: new HttpParams().set('amhpname', searchTerm) };

    return this.httpClient.get(
      environment.apiEndpoint + '/user/search', options
    ).pipe(
      map(response => response)
    );
  }
}
