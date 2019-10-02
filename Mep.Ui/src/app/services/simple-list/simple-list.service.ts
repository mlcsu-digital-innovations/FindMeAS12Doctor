import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
import { SimpleList } from 'src/app/interfaces/simple-list';

@Injectable({
  providedIn: 'root'
})
export class SimpleListService {

  constructor(private httpClient: HttpClient) { }

  public GetList(listType: string): Observable<SimpleList> {

    return this.httpClient.get(
      environment.apiEndpoint + listType
    ).pipe(
      map(response => {response)
    );
  }
}
