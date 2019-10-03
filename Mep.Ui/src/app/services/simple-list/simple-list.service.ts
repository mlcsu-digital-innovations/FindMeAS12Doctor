import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
import { SimpleList } from 'src/app/interfaces/simple-list';

@Injectable({
  providedIn: 'root'
})
export class SimpleListService {

  constructor(private httpClient: HttpClient) {
  }

  GetListData(listType: string): Observable<SimpleList[]> {
    return this.httpClient.get<SimpleList[]>(`${environment.apiEndpoint}/${listType}`)
    .pipe(
      map(listValues => listValues)
    );
  }
}
