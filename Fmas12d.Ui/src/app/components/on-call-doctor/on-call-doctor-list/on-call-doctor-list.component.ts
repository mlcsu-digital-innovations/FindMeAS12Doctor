import { Component, OnInit, ViewChild, QueryList, ViewChildren } from '@angular/core';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { OnCallDoctor } from 'src/app/interfaces/on-call-doctor';
import { OnCallDoctorList } from 'src/app/interfaces/on-call-doctor-list';
import { OnCallDoctorListService } from 'src/app/services/on-call-doctor-list/on-call-doctor-list.service';
import { OnCallDoctorService } from 'src/app/services/on-call-doctor/on-call-doctor.service';
import { RouterService } from 'src/app/services/router/router.service';
import { SortEvent, TableHeaderSortable } from 'src/app/directives/table-header-sortable/table-header-sortable.directive';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-on-call-doctor-list',
  templateUrl: './on-call-doctor-list.component.html',
  styleUrls: ['./on-call-doctor-list.component.css']
})
export class OnCallDoctorListComponent implements OnInit {
  deleteModal: NgbModalRef;
  noOfOnCallDoctorsInList: number;
  onCallDoctorListModal: NgbModalRef;
  onCallDoctorList$: Observable<OnCallDoctorList[]>;
  selectedOnCallDoctor: OnCallDoctorList;
  total$: Observable<number>;

  @ViewChild('addOnCallDoctorModal', null) addOnCallDoctorTemplate;
  @ViewChild('editOnCallDoctorModal', null) editOnCallDoctorTemplate;
  @ViewChild('deleteOnCallDoctorModal', null) deleteOnCallDoctorTemplate;
  @ViewChildren(TableHeaderSortable) headers: QueryList<TableHeaderSortable>;

  constructor(
    private modalService: NgbModal,
    private onCallDoctorService: OnCallDoctorService,
    public onCallDoctorListService: OnCallDoctorListService,
    private routerService: RouterService,
    private toastService: ToastService
  ) { }

  ngOnInit() {
    this.onCallDoctorList$ = this.onCallDoctorListService.onCallDoctorList$;
    this.onCallDoctorList$.subscribe((result: OnCallDoctorList[]) => {
      this.noOfOnCallDoctorsInList = result.length;
    }, err => {
      this.toastService.displayError({ message: err });
    });
    this.total$ = this.onCallDoctorListService.total$;
  }

  addOnCallDoctor(): void {
    this.onCallDoctorListModal = this.modalService.open(
      this.addOnCallDoctorTemplate,
      { size: 'lg' }
    );
  }

  deleteOnCallDoctor(onCallDoctor: OnCallDoctorList): void {
    this.selectedOnCallDoctor = onCallDoctor;
    this.deleteModal = this.modalService.open(this.deleteOnCallDoctorTemplate, {
      size: 'lg'
    });
  }

  editOnCallDoctor(onCallDoctor: OnCallDoctorList): void {
    this.selectedOnCallDoctor = onCallDoctor;
    this.onCallDoctorListModal = this.modalService.open(
      this.editOnCallDoctorTemplate,
      { size: 'lg' }
    );
  }

  OnModalActionAdd(onCallDoctor: OnCallDoctor) {
    this.onCallDoctorListModal.close();
    if (onCallDoctor) {
      this.onCallDoctorService.addOnCallDoctor(onCallDoctor).subscribe(result => {
        this.toastService.displaySuccess({ message: 'On Call Doctor added' });
        this.onCallDoctorListService.InitialiseService();
      }, err => {
        this.toastService.displayInfo({ message: 'On Call Doctor add Error' });
      });
    } else {
      this.toastService.displayInfo({ message: 'On Call Doctor add has been cancelled' });
    }
  }

  OnModalActionEdit(onCallDoctor: OnCallDoctor) {
    this.onCallDoctorListModal.close();
    if (onCallDoctor) {
      this.onCallDoctorService.editOnCallDoctor(onCallDoctor).subscribe(result => {
        this.toastService.displaySuccess({ message: 'On Call Doctor updated' });
        this.onCallDoctorListService.InitialiseService();
      }, err => {
        this.toastService.displayInfo({ message: 'On Call Doctor Update error' });
      });
    } else {
      this.toastService.displayInfo({ message: 'On Call Doctor update has been cancelled' });
    }
  }

  OnDeleteOnCallDoctorAction(action: boolean) {
    this.deleteModal.close();

    if (action) {
      this.onCallDoctorService.removeOnCallDoctor(this.selectedOnCallDoctor.id)
        .subscribe(result => {
        this.toastService.displaySuccess({ message: 'On Call Doctor deleted' });
        this.onCallDoctorListService.InitialiseService();
      }, err => {
        this.toastService.displayInfo({ message: 'On Call Doctor delete error' });
      });
    } else {
      this.toastService.displayInfo({ message: 'On Call Doctor delete has been cancelled' });
    }
  }

  onSort({ column, direction, columnType }: SortEvent) {
    this.headers.forEach(header => {
      if (header.sortable !== column) {
        header.direction = '';
      }
    });

    this.onCallDoctorListService.sortColumn = column;
    this.onCallDoctorListService.sortDirection = direction;
    this.onCallDoctorListService.sortColumnType = columnType;
  }
}
