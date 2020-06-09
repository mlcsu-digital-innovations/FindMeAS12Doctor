import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserAvailability } from 'src/app/interfaces/user-availability';
import { environment } from 'src/environments/environment';
import { UserAvailabilityOverlapping } from 'src/app/interfaces/user-availability-overlapping';

@Injectable({
  providedIn: 'root'
})
export class UserAvailabilityService {

  constructor(private http: HttpClient) { }
  
  public checkOverlapping(userAvailability: UserAvailability): Observable<UserAvailabilityOverlapping> {
    let endpoint = `${environment.apiEndpoint}/useravailabilities/overlapping/${userAvailability.userId}?` +
      `start=${userAvailability.start.toISOString()}&` +
      `end=${userAvailability.end.toISOString()}&` +
      `userAvailabilityId=${userAvailability.id}`;
    return this.http.get<UserAvailabilityOverlapping>(endpoint);  
  }
}
