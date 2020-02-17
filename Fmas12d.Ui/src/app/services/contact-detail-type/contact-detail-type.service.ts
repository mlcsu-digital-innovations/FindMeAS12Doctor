import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ContactDetailTypeService {

  constructor(private httpClient: HttpClient) {}

  public GetContactDetailTypes(userId: number): Observable<any> {
    return this.httpClient.get(
      `${environment.apiEndpoint}/contactdetailtype/${userId}`
    );
  }
}
