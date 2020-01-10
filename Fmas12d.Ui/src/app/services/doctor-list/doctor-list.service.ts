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

  public GetDoctorList(searchTerm: string, includeUnregisteredDoctors: boolean) {

    if (searchTerm === '' || searchTerm.length < 3) {
      return of([]);
    }
    const paramName: string = isNaN(+searchTerm) ? 'doctorname' : 'gmcnumber';

    const options = {
      params: new HttpParams()
        .set(paramName, searchTerm)
        .set('includeUnregisteredDoctors', String(includeUnregisteredDoctors))
    };

    return this.httpClient.get(
      environment.apiEndpoint + '/user/search', options
    ).pipe(
      map(response => response)
    );
  }
}
