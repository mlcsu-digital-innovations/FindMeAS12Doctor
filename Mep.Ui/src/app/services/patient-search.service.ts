import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PatientSearch } from '../classes/patient-search';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PatientSearchService {
  constructor(private httpClient: HttpClient) {}

  public patientSearch(searchParams: PatientSearch) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      environment.apiEndpoint + '/patient/search',
      searchParams,
      { headers }
    );
  }
}
