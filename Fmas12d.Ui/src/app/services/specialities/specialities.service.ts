import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Speciality } from 'src/app/interfaces/speciality';

@Injectable({
  providedIn: 'root'
})
export class SpecialitiesService {

  constructor() { }

  public GetSpecialities(): Observable<any> {
    let specialities: Speciality[] = [
      {
        id: 1,
        name: 'Adult MH'
      },
      {
        id: 2,
        name: 'Children'
      },
      {
        id: 3,
        name: 'Learning Disability'
      },
      {
        id: 4,
        name: 'Neuropsychological'
      },
      {
        id: 5,
        name: 'Older People MH'
      }
    ];
    return of(specialities);
  }
}
