import { environment } from 'src/environments/environment';
import { Assessment } from 'src/app/interfaces/assessment';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { AssessmentAvailability } from 'src/app/interfaces/assessment-availability';

@Injectable({
  providedIn: 'root'
})
export class AssessmentService {

  constructor(private httpClient: HttpClient) { }

  public createAssessment(assessment: Assessment) {

    let assessmentType = '';
    if (assessment.isPlanned) {
      assessmentType = '/planned';
    } else {
      assessmentType = '/emergency';
    }

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      environment.apiEndpoint + '/assessment' + assessmentType,
      assessment,
      { headers }
    );
  }

  public getAvailableDoctors(assessmentId: number) {

    return this.httpClient.get(
      `${environment.apiEndpoint}/assessment/${assessmentId}/doctors/available`
    ).pipe(
      map(response => response)
    );
  }

  public updateSelectedDoctors(assessmentId: number, userIds: any) {

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      `${environment.apiEndpoint}/assessment/${assessmentId}/doctors/selected`,
      userIds,
      { headers }
    );
  }

}
