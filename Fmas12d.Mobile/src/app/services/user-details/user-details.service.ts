import { ApiService } from '../api/api.service';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserDetails } from 'src/app/interfaces/user-details';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  private userDetails: UserDetails;
  public currentUser: ReplaySubject<UserDetails> = new ReplaySubject<UserDetails>(1);

  constructor(
    private apiService: ApiService
  ) { }

  public getUserDetails(userId: string): Observable<UserDetails> {
    return (this.apiService.get(
      `${environment.apiEndpoint}/user/identifier/${userId}`,
      null
    ) as Observable<UserDetails>)
    .pipe(
      map((result: UserDetails) => {
        this.userDetails = result;
        this.currentUser.next(result);
        return result;
      })
    );
  }

  public refreshFcmToken(token: string) {
    return (this.apiService.put(
      `${environment.apiEndpoint}/user/refreshToken/${token}`,
      null
    ));
  }

  public fetchUserDetails(): UserDetails {
    return this.userDetails;
  }
}
