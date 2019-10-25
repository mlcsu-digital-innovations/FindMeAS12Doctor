import { Component, OnInit } from '@angular/core';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { DoctorListService } from 'src/app/services/doctor-list/doctor-list.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { tap, switchMap, catchError } from 'rxjs/operators';

@Component({
  selector: 'app-doctor-select',
  templateUrl: './doctor-select.component.html',
  styleUrls: ['./doctor-select.component.css']
})
export class DoctorSelectComponent implements OnInit {

  doctorForm: FormGroup;
  hasDoctorSearchFailed: boolean;
  isDoctorFieldsShown: boolean;
  isDoctorSearching: boolean;
  unknownDoctorId: number;

  constructor(
    private doctorListService: DoctorListService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.unknownDoctorId = 0;

    this.doctorForm = this.formBuilder.group({
      searchDoctor: []
    });

  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  get doctorField() {
    return this.doctorForm.controls.searchDoctor;
  }

  DoctorSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isDoctorSearching = true)),
      switchMap(term =>
        this.doctorListService.GetDoctorList(term).pipe(
          tap(() => (this.hasDoctorSearchFailed = false)),
          catchError(() => {
            this.hasDoctorSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.isDoctorSearching = false))
    )
}
