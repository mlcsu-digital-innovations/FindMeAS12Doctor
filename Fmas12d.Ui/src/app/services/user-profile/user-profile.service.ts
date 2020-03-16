import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { UserProfile } from 'src/app/interfaces/user-profile';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {
  private _loading$ = new BehaviorSubject<boolean>(true);  

  constructor(private http: HttpClient) {}

  get loading$() { return this._loading$.asObservable(); }
  loading(newValue: boolean) { this._loading$.next(newValue); }

  GetUser(): Observable<UserProfile> {
    let endpoint = environment.apiEndpoint + '/userprofile';
    return this.http.get<UserProfile>(endpoint);
  }

  UpdateUser(user: UserProfile): Observable<UserProfile> {
    let endpoint = environment.apiEndpoint + '/userprofile';    
    return this.http.put<UserProfile>(endpoint, user);
  }

}
