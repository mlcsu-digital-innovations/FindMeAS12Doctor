import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { PatientSearchParams } from '../../interfaces/patient-search-params';

@Injectable({
  providedIn: 'root'
})
export class PatientSearchService {
  constructor(private httpClient: HttpClient) {}

  public patientSearch(searchParams: PatientSearchParams) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      environment.apiEndpoint + '/patient/search',
      searchParams,
      { headers }
    );
  }
}
