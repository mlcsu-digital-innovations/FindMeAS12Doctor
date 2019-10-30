import { AmhpExaminationList } from '../../models/amhp-examination-list.model';
import { AmhpExaminationOutcome } from 'src/app/models/amhp-examination-outcome.model';
import { AmhpExaminationView } from '../../models/amhp-examination-view.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AmhpExaminationService {
  private examinationView: AmhpExaminationView;

  constructor(private http: HttpClient) { }

  public getList(amhpUserId: number): Observable<AmhpExaminationList[]> {
    return this.http
      .get<AmhpExaminationList[]>(
        `${environment.apiEndpoint}/examination/list?amhpUserId=${amhpUserId}`
      )
      .pipe(map(result => this.examinationListSort(result)));
  }

  public getView(examinationId: string): Observable<AmhpExaminationView> {
    return this.http
      .get<AmhpExaminationView>(
        `${environment.apiEndpoint}/examination/view/${examinationId}`
      )
      .pipe(map(result => result));
  }

  public putOutcome(
    examinationOutcome: AmhpExaminationOutcome,
    examinationId: number,
    success: boolean):
    Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    let url: string = `${environment.apiEndpoint}/examination/outcome/${examinationId}/${success ?
      'success' : 'failure'}`;

    return this.http.put(
      url,
      examinationOutcome,
      { headers }
    );
  }

  public storeView(examinationView: AmhpExaminationView): void {
    this.examinationView = examinationView;
  }

  public retrieveView(): AmhpExaminationView {
    return this.examinationView;
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
