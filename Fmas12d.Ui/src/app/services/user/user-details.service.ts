import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/interfaces/user';
import { tap, map, share } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  public userType: ReplaySubject<number> = new ReplaySubject<number>(1);

  constructor(private httpClient: HttpClient) {
  }

 public getCurrentUserDetails(): Observable<User> {
  const userQuery =  (this.httpClient.get(`${environment.apiEndpoint}/user`) as Observable<User>);
  return userQuery.pipe(
    tap((user: User) => {
      this.userType.next(user.profileTypeId);
    }),
    map(ex => ex)
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
