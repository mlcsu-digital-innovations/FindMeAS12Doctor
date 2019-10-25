import { Component, OnInit } from '@angular/core';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { GpPracticeListService } from '../../../services/gp-practice-list/gp-practice-list.service';
import { tap, switchMap, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-doctor-select',
  templateUrl: './doctor-select.component.html',
  styleUrls: ['./doctor-select.component.css']
})
export class DoctorSelectComponent implements OnInit {

  hasDoctorSearchFailed: boolean;
  isDoctorFieldsShown: boolean;
  isDoctorSearching: boolean;
  unknownDoctorId: number;
  selectDoctor: FormGroup;

  constructor(
    private doctorListService: GpPracticeListService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.unknownDoctorId = 0;
    this.selectDoctor = this.formBuilder.group({
      doctorSearch: []
     });
  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  DoctorSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isDoctorSearching = true)),
      switchMap(term =>
        this.doctorListService.GetGpPracticeList(term).pipe(
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
