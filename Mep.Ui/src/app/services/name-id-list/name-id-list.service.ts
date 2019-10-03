import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
import { NameIdList } from 'src/app/interfaces/name-id-list';

@Injectable({
  providedIn: 'root'
})
export class NameIdListService {

  constructor(private httpClient: HttpClient) {
  }

  GetListData(listType: string): Observable<NameIdList[]> {
    return this.httpClient.get<NameIdList[]>(`${environment.apiEndpoint}/${listType}`)
    .pipe(
      map(listValues => listValues)
    );
  }
}
