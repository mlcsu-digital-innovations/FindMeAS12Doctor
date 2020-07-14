import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/interfaces/user';
import { tap, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  private currentUserProfileTypeId: number;

  constructor(private httpClient: HttpClient) {
  }

  public getCurrentUSerProfileType(): number {
    return this.currentUserProfileTypeId;
  }

 public getCurrentUserDetails(): Observable<User> {
  const userQuery =  (this.httpClient.get(`${environment.apiEndpoint}/user`) as Observable<User>);
  return userQuery.pipe(
    tap((user: User) => {
      this.currentUserProfileTypeId = user.profileTypeId;
    }),
    map(user => user)
  );
}

  public GetDoctorDetails(doctorId: number) {
    return this.httpClient.get(
      `${environment.apiEndpoint}/user/${doctorId}`
    );
  }

  public GetS12DoctorDetails(doctorId: number) {
    return this.httpClient.get(
      `${environment.apiEndpoint}/user/s12register/${doctorId}`
    );
  }
}
