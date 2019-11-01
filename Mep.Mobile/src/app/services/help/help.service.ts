import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HelpService {

  constructor(private http: HttpClient) { }

  public getApiVersion(): Observable<string> {
    return this.http.get(`${environment.apiEndpoint}/version`, { responseType: 'text' })
      .pipe(map(result => result));
  }
}
