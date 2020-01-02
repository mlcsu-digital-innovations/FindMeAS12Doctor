import { ApiService } from '../api/api.service';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  constructor(
    private apiService: ApiService
  ) { }

  public getUserDetails(userId: string) {
    return (this.apiService.get(
      `${environment.apiEndpoint}/user/identifier/${userId}`,
      null
    ));
  }
}
