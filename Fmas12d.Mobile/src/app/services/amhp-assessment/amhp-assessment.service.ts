import { AmhpExaminationList } from '../../models/amhp-assessment-list.model';
import { AmhpExaminationOutcome } from 'src/app/models/amhp-assessment-outcome.model';
import { AmhpExaminationView } from '../../models/amhp-assessment-view.model';
import { ApiService } from '../api/api.service';
import { Injectable } from '../../pages/amhp-assessment-list/node_modules/@angular/core';
import { Observable } from '../../pages/amhp-assessment-list/node_modules/rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AmhpExaminationService {
  private examinationView: AmhpExaminationView;

  constructor(
    private apiService: ApiService
  ) { }

  public getList(amhpUserId: number): Observable<AmhpExaminationList[]> {   
    return (this.apiService.get(
      `${environment.apiEndpoint}/examination/list?amhpUserId=${amhpUserId}`, 
      'AmhpUserList'
    ) as Observable<AmhpExaminationList[]>)
    .pipe(
      map(result => this.examinationListSort(result)),
    );
  }

  public getView(examinationId: string): Observable<AmhpExaminationView> {  
    return this.apiService.get(
      `${environment.apiEndpoint}/examination/view/${examinationId}`,
      `AmhpUserView-${examinationId}`
    ) as Observable<AmhpExaminationView>;
  }

  public putOutcome(
    examinationOutcome: AmhpExaminationOutcome,
    examinationId: number,
    success: boolean):
    Observable<any> {
    let url: string = `${environment.apiEndpoint}/examination/outcome/${examinationId}/${success ?
      'success' : 'failure'}`;

    return this.apiService.put(url, examinationOutcome);  
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
