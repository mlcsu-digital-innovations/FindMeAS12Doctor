import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PatientSearchParams } from '../../interfaces/patient-search-params';
import { map } from 'rxjs/operators';
import { PatientSearchResult } from 'src/app/interfaces/patient-search-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientSearchService {
  constructor(private httpClient: HttpClient) { }

  public patientSearch(searchParams: PatientSearchParams): Observable<PatientSearchResult> {

    let options: { params: HttpParams };
    if (searchParams.nhsNumber) {
      options = {
        params: new HttpParams().set('nhsnumber', searchParams.nhsNumber.toString())
      };
    } else {
      options = {
        params: new HttpParams().set('alternativeidentifier', searchParams.alternativeIdentifier)
      };
    }

    return this.httpClient.get<PatientSearchResult>(
      environment.apiEndpoint + '/patient/search', options
    ).pipe(
      map(results => results)
    );
  }
}
