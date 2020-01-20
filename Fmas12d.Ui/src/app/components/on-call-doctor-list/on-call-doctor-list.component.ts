import { Component, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { OnCallDoctorList } from 'src/app/interfaces/on-call-doctor-list';
import { OnCallDoctorListService } from 'src/app/services/on-call-doctor-list/on-call-doctor-list.service';

@Component({
  selector: 'app-on-call-doctor-list',
  templateUrl: './on-call-doctor-list.component.html',
  styleUrls: ['./on-call-doctor-list.component.css']
})
export class OnCallDoctorListComponent implements OnInit {
  deleteModal: NgbModalRef;
  onCallDoctorList$: Observable<OnCallDoctorList[]>;

  constructor(
    private onCallDoctorListService: OnCallDoctorListService,
  ) { }

  ngOnInit() {
    this.onCallDoctorList$ = this.onCallDoctorListService.onCallDoctorList$;        
  }

  add(): void {
    
  }

  edit(): void {
    
  }

  delete(): void {
    
  }
}
