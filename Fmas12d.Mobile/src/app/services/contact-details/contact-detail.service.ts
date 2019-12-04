import { ApiService } from '../api/api.service';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class ContactDetailService {

  constructor(
    private apiService: ApiService
  ) { }

  public getContactDetails() {
    return (this.apiService.get(
      `${environment.apiEndpoint}/contactdetailtype`,
      null
    ));
  }

  public getContactDetailsForAssessment(assessmentId: number) {
    return (this.apiService.get(
      `${environment.apiEndpoint}/contactdetailtype/assessments/${assessmentId}`,
      null
    ));
  }
}
