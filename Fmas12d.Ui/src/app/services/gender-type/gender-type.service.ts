import { Injectable } from '@angular/core';
import { GenderType } from 'src/app/interfaces/gender-type';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenderTypeService {

  constructor() { }

  public GetGenderTypes(): Observable<any> {
    let genderTypes: GenderType[] = [
      {
        id: 1,
        name: 'Female'
      },
      {
        id: 2,
        name: 'Male'
      },
      {
        id: 3,
        name: 'Other'
      }
    ];
    return of(genderTypes);
  }
}
