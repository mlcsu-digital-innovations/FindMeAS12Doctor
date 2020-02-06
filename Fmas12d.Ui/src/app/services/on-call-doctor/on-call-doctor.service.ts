import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OnCallDoctor } from 'src/app/interfaces/on-call-doctor';
import { OnCallDoctorList } from 'src/app/interfaces/on-call-doctor-list';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OnCallDoctorService {

  constructor(private http: HttpClient) { }

  public addOnCallDoctor(onCallDoctor: OnCallDoctor) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    let action: string = 'contactdetail';
    if (!onCallDoctor.contactDetailId) {
      action = 'postcode';
    }
    let endpoint = `${environment.apiEndpoint}/user/oncall/${action}`;
    return this.http.post<OnCallDoctorList[]>(endpoint, onCallDoctor, { headers });  
  }

  public editOnCallDoctor(onCallDoctor: OnCallDoctor) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    let action: string = 'contactdetail';
    if (!onCallDoctor.contactDetailId) {
      action = 'postcode';
    }
    let endpoint = `${environment.apiEndpoint}/user/oncall/${onCallDoctor.id}/${action}`;
    return this.http.put<OnCallDoctorList[]>(endpoint, onCallDoctor, { headers });  
  }

  public removeOnCallDoctor(id: number) {
    let endpoint = `${environment.apiEndpoint}/user/oncall/${id}`
    return this.http.delete(endpoint);
  }
}
