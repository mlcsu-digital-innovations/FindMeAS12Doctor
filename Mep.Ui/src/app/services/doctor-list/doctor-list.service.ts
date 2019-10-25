import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DoctorListService {

  constructor(private httpClient: HttpClient) {}

  public GetDoctorList(searchTerm: string) {

    if (searchTerm === '' || searchTerm.length < 3) {
      return of([]);
    }
    const options = { params: new HttpParams().set('search', searchTerm) };

    return this.httpClient.get(
      environment.apiEndpoint + '/doctorsearch', options
    ).pipe(
      map(response => response)
    );
  }
}
