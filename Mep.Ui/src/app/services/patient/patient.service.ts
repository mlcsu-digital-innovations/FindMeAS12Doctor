import { Injectable } from '@angular/core';
import { Patient } from '../../interfaces/patient';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  constructor(private httpClient: HttpClient) {}

  public createPatient(newPatient: Patient ) {

    console.log(newPatient);

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      environment.apiEndpoint + '/patient',
      newPatient,
      { headers }
    );
  }
}
