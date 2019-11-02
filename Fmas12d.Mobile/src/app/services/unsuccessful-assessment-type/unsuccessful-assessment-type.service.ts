import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UnsuccessfulAssessmentType } from 'src/app/models/unsuccessful-assessment-type.model';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UnsuccessfulAssessmentTypeService {

  constructor(private http: HttpClient) { }

  public getList(): Observable<UnsuccessfulAssessmentType[]> {
    return this.http
      .get<UnsuccessfulAssessmentType[]>(
        `${environment.apiEndpoint}/unsuccessfulassessmenttype/list`
      )
      .pipe(map(result => result));
  }
}
