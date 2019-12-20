import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {
  constructor(private httpClient: HttpClient) {}

  public GetDoctorDetails(doctorId: number) {
    return this.httpClient.get(
      `${environment.apiEndpoint}/user/${doctorId}`
    );
  }
}
