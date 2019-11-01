import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UnsuccessfulExaminationType } from 'src/app/models/unsuccessful-examination-type.model';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UnsuccessfulExaminationTypeService {

  constructor(private http: HttpClient) { }

  public getList(): Observable<UnsuccessfulExaminationType[]> {
    return this.http
      .get<UnsuccessfulExaminationType[]>(
        `${environment.apiEndpoint}/unsuccessfulexaminationtype/list`
      )
      .pipe(map(result => result));
  }
}
