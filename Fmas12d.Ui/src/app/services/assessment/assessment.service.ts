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

    const assessmentType = assessment.isPlanned
      ? 'planned'
      : 'emergency';

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      `${environment.apiEndpoint}/assessment/${assessmentType}`,
      assessment,
      { headers }
    );
  }

  public updateAssessment(assessment: Assessment) {

    const assessmentType = assessment.isPlanned
      ? 'planned'
      : 'emergency';

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.put(
      `${environment.apiEndpoint}/assessment/${assessment.id}/${assessmentType}`,
      assessment,
      { headers }
    );
  }

  public scheduleAssessment(assessmentId: number, scheduledTime: Date) {

    return this.httpClient.put(
      `${environment.apiEndpoint}/assessment/${assessmentId}/schedule`,
      {scheduledTime}
    );
  }

  public getAvailableDoctors(assessmentId: number) {

    return this.httpClient.get(
      `${environment.apiEndpoint}/assessment/${assessmentId}/doctors/available`
    ).pipe(
      map(response => response)
    );
  }

  public getSelectedDoctors(assessmentId: number) {

    return this.httpClient.get(
      `${environment.apiEndpoint}/assessment/${assessmentId}/doctors/selected`
    ).pipe(
      map(response => response)
    );
  }

  public updateAllocatedDoctors(assessmentId: number, userIds: any) {

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      `${environment.apiEndpoint}/assessment/${assessmentId}/doctors/allocated`,
      userIds,
      { headers }
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

  public allocateDoctorDirectly(assessmentId: number, userId: number) {

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    const user = {
      userId
    };

    return this.httpClient.post(
      `${environment.apiEndpoint}/assessment/${assessmentId}/doctors/allocated/direct`,
      user,
      { headers }
    );
  }

}
