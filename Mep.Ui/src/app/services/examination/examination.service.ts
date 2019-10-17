import { environment } from 'src/environments/environment';
import { Examination } from 'src/app/interfaces/examination';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ExaminationService {

  constructor(private httpClient: HttpClient) { }

  public createExamination(examination: Examination) {

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      environment.apiEndpoint + '/examination',
      examination,
      { headers }
    );
  }
}
