import { ApiService } from '../api/api.service';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserDetails } from 'src/app/interfaces/user-details';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  private userDetails: UserDetails;

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
        return result;
      })
    );
  }

  public fetchUserDetails(): UserDetails {
    return this.userDetails;
  }
}
