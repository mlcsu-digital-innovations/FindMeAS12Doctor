import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AmhpExaminationList } from '../../models/amhp-examination-list.model';
import { AmhpExaminationView } from '../../models/amhp-examination-view.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AmhpExaminationService {

  constructor(private http: HttpClient) { }

  getList(amhpUserId: number): Observable<AmhpExaminationList[]> {
    return this.http
      .get<AmhpExaminationList[]>(
        `${environment.apiEndpoint}/examination/list?amhpUserId=${amhpUserId}`
      )
      .pipe(map(result => this.examinationListSort(result)));
  }

  getView(examinationId: string): Observable<AmhpExaminationView> {
    return this.http
      .get<AmhpExaminationView>(       
        `${environment.apiEndpoint}/examination/view/${examinationId}`
      )
      .pipe(map(result => result));
  }

  private examinationListSort(examinationList: AmhpExaminationList[]): AmhpExaminationList[] {
    return examinationList.sort(
      (examination1: AmhpExaminationList, examination2: AmhpExaminationList) => {
      if (examination1.dateTime > examination2.dateTime) {
        return 1;
      }
      else if (examination1.dateTime < examination2.dateTime) {
        return -1;
      }
      return 0;
    });
  }
}
