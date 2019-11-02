import { environment } from 'src/environments/environment';
import { Examination } from 'src/app/interfaces/assessment';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ExaminationService {

  constructor(private httpClient: HttpClient) { }

  public createExamination(examination: Examination) {

    let examinationType = '';
    if (examination.isPlanned) {
      examinationType = '/planned';
    } else {
      examinationType = '/emergency';
    }

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      environment.apiEndpoint + '/examination' + examinationType,
      examination,
      { headers }
    );
  }
}
