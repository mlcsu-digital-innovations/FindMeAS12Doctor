import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PatientSearch } from '../classes/patient-search';

@Injectable({
  providedIn: 'root'
})
export class PatientSearchService {

  constructor(private httpClient: HttpClient) { }

  public patientSearch(searchParams: PatientSearch) {

  }
}
