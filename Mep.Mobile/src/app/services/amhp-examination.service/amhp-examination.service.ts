import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AmhpExaminationList } from '../../models/amhp-examination-list.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AmhpExaminationService {

  constructor(private http: HttpClient) { }

  getList(amhpUserId: number): Observable<AmhpExaminationList[]> {
    return this.http
      .get<AmhpExaminationList[]>(
        environment.apiEndpoint +
        "examination/list?amhpUserId=" +
        amhpUserId
      );
  }
}