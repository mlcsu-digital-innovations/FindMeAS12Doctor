import { Component, OnInit } from '@angular/core';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { DoctorListService } from 'src/app/services/doctor-list/doctor-list.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { tap, switchMap, catchError } from 'rxjs/operators';
import { Assessment } from 'src/app/interfaces/assessment';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { AssessmentService } from 'src/app/services/assessment/assessment.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UserAvailabilityService } from 'src/app/services/user-availability/user-availability.service';
import { AvailableDoctor } from 'src/app/interfaces/available-doctor';
import { EventManager } from '@angular/platform-browser';

@Component({
  selector: 'app-doctor-select',
  templateUrl: './doctor-select.component.html',
  styleUrls: ['./doctor-select.component.css']
})
export class DoctorSelectComponent implements OnInit {

  assessment$: Observable<Assessment | any>;
  availableDoctors: AvailableDoctor[];
  availableDoctors$: Observable<AvailableDoctor[] | any>;
  doctorForm: FormGroup;
  hasDoctorSearchFailed: boolean;
  isAvailableDoctorSearching: boolean;
  isDoctorFieldsShown: boolean;
  isDoctorSearching: boolean;
  unknownDoctorId: number;
  selectDoctor: FormGroup;

  constructor(
    private assessmentService: AssessmentService,
    private doctorListService: DoctorListService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private toastService: ToastService,
    private userAvailabilityService: UserAvailabilityService
  ) { }

  ngOnInit() {
    this.unknownDoctorId = 0;

    this.doctorForm = this.formBuilder.group({
      searchDoctor: [],
      doctorDistance: []
    });

    this.assessment$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.assessmentService.getAssessment(+params.get('assessmentId'))
            .pipe(
              map(assessment => {

                // this.referralCreated = referral.createdAt;
                // this.referralId = +params.get('referralId');

                // this.minDate = this.ConvertToDateStruct(referral.createdAt);
                // this.SetDefaultDateTimeFields(referral.defaultToBeCompletedBy);
                console.log(assessment);
                return assessment;
              })
            );
        }
      ),
      catchError((err) => {

        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving Referral Information'
        });

        const emptyAssessment = {} as Assessment;
        return of(emptyAssessment);
      })
    );

    this.FetchAvailableDoctors(10);

    this.OnChanges();
  }

  FetchAvailableDoctors(maxDistance: number) {
    this.isAvailableDoctorSearching = true;
    this.userAvailabilityService.getAvailableDoctors(maxDistance)
    .subscribe(x => {
      this.isAvailableDoctorSearching = false;
      this.availableDoctors = x;
      this.availableDoctors.sort((a, b) => (a.distanceFromAssessment > b.distanceFromAssessment) ? 1 : -1);
    }
    , (err) => {
      this.isAvailableDoctorSearching = false;
      this.toastService.displayError({
        title: 'Search Error',
        message: 'Error Retrieving Available Doctors'
      });
    });
  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  get doctorField() {
    return this.doctorForm.controls.searchDoctor;
  }

  get doctorDistance() {
    return this.doctorForm.controls.doctorDistance;
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

    OnChanges(): void {
      this.doctorDistance.valueChanges.subscribe(val => {
        this.FetchAvailableDoctors(val);
      });
    }

    OnSort(event: any) {
      if (event.direction === 'desc') {
        this.availableDoctors.sort((a, b) => (a[event.column] > b[event.column]) ? -1 : 1);
      } else {
        this.availableDoctors.sort((a, b) => (a[event.column] > b[event.column]) ? 1 : -1);
      }
    }
}
