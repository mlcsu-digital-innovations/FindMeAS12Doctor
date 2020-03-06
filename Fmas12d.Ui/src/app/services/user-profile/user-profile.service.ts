import { Injectable } from '@angular/core';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { UserProfile } from 'src/app/interfaces/user-profile';
import { HttpClient } from '@angular/common/http';
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
    return of({} as UserProfile);
  }

}
