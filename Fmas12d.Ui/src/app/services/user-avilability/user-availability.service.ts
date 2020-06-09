import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserAvailability } from 'src/app/interfaces/user-availability';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserAvailabilityService {

  constructor(private http: HttpClient) { }
  
  public checkOverlapping(userAvailability: UserAvailability): Observable<string> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    let endpoint = `${environment.apiEndpoint}/useravailabilities/overlapping/${userAvailability.userId}`;
    return this.http.post<string>(endpoint, userAvailability, { headers });  
  }
}
