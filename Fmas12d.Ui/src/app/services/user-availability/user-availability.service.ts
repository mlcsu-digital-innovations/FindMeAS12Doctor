import { environment } from '../../../environments/environment';
import { filter, delay, map } from 'rxjs/operators';
import { from, Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AvailableDoctor } from 'src/app/interfaces/available-doctor';

@Injectable({
  providedIn: 'root'
})
export class UserAvailabilityService {

  constructor(private httpClient: HttpClient) {}

  public getAvailableDoctors(maxDistance: number): Observable<AvailableDoctor[]> {

    // Dummy data !!
    const doctors = of([
      {id: 1, doctorName: 'Doctor Smith', doctorGender: 'Male', doctorType: 'Consultant', doctorSpeciality: 'LD', distanceFromAssessment: 1.0, availabilityDetails: '09:00 - 17:00'},
      {id: 2, doctorName: 'Doctor Jones', doctorGender: 'Male', doctorType: 'Consultant', doctorSpeciality: '', distanceFromAssessment: 2.0},
      {id: 3, doctorName: 'Doctor Davies', doctorGender: 'Male', doctorType: 'Consultant', doctorSpeciality: '', distanceFromAssessment: 3.0},
      {id: 4, doctorName: 'Doctor Hill', doctorGender: 'Female', doctorType: 'Consultant', doctorSpeciality: 'LD', distanceFromAssessment: 4.0},
      {id: 5, doctorName: 'Doctor Johnson', doctorGender: 'Female', doctorType: '', doctorSpeciality: 'Children', distanceFromAssessment: 5.0},
      {id: 6, doctorName: 'Doctor Tyler', doctorGender: 'Female', doctorType: 'Consultant', doctorSpeciality: 'LD', distanceFromAssessment: 6.0},
      {id: 7, doctorName: 'Doctor Burton', doctorGender: 'Male', doctorType: 'Consultant', doctorSpeciality: '', distanceFromAssessment: 7.0, otherAssessmentDetails: 'Scheduled for assessment: PO32 6EY @ 17:45'},
      {id: 8, doctorName: 'Doctor Francis', doctorGender: 'Male', doctorType: '', doctorSpeciality: '', distanceFromAssessment: 8.0},
      {id: 9, doctorName: 'Doctor Hunter', doctorGender: 'Male', doctorType: '', doctorSpeciality: '', distanceFromAssessment: 9.0},
      {id: 10, doctorName: 'Doctor Matthews', doctorGender: 'Male', doctorType: 'Consultant', doctorSpeciality: '', distanceFromAssessment: 10.0},
      {id: 11, doctorName: 'Doctor Lane', doctorGender: 'Female', doctorType: '', doctorSpeciality: '', distanceFromAssessment: 10.5}
    ]);

    return doctors
      .pipe(delay(2000))
      .pipe(map(x => x.filter(y => y.distanceFromAssessment <= maxDistance)));
  }
}
