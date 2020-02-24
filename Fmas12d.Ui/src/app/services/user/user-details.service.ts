import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {
  constructor(private httpClient: HttpClient) {}

  public getCurrentUserDetails(): Observable<User> {
    return this.httpClient.get(
      `${environment.apiEndpoint}/user`
    ) as Observable<User>
  }

  public GetDoctorDetails(doctorId: number) {
    return this.httpClient.get(
      `${environment.apiEndpoint}/user/${doctorId}`
    );
  }
}
