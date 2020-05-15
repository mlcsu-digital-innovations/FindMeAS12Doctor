import { Component, QueryList, ViewChildren, OnInit } from '@angular/core';
import { DoctorClaimExport } from 'src/app/interfaces/doctor-claim-export';
import { DoctorClaimListService } from 'src/app/services/doctor-claim-list/doctor-claim-list.service';
import { ExcelService } from 'src/app/services/excel-service/excel.service';
import { Observable } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { TableHeaderSortable, SortEvent } from '../../../directives/table-header-sortable/table-header-sortable.directive';
import { ToastService } from '../../../services/toast/toast.service';
import { UserAssessmentClaim } from 'src/app/interfaces/user-assessment-claim';
import * as moment from 'moment';
@Component({
  selector: 'app-doctor-claim-list',
  styleUrls: ['./doctor-claim-list.component.css'],
  templateUrl: './doctor-claim-list.component.html'
})
export class DoctorClaimListComponent implements OnInit {

  claimsList$: Observable<UserAssessmentClaim[]>;
  displayedList: UserAssessmentClaim[];
  error: any;
  exportData: DoctorClaimExport[];
  total$: Observable<number>;

  @ViewChildren(TableHeaderSortable) headers: QueryList<TableHeaderSortable>;

  constructor(
    public claimsService: DoctorClaimListService,
    private excelService: ExcelService,
    private toastService: ToastService,
    public oidcSecurityService: OidcSecurityService,
  ) {
  }

  ngOnInit() {

    this.claimsList$ = this.claimsService.getClaims(true);
    this.total$ = this.claimsService.total$;

    this.claimsList$.subscribe(
      result => {
        this.displayedList = result;
      },
      error => {
        this.toastService.displayError({
          title: 'Error',
          message: error
        });
      }
    );
  }

  exportClaims() {

    this.exportData = [];
    this.displayedList.forEach(claim => {
      this.exportData.push({
          claimReference: claim.claimReference,
          claimStatus: claim.claimStatus.name,
          assessmentDate: moment(claim.assessment.scheduledTime).toDate(),
          assessmentPostcode: claim.assessment.postcode,
          successfulAssessment: claim.assessment.isSuccessful,
          mileage: claim.mileage,
          mileagePayment: claim.mileagePayment,
          assessmentPayment: claim.assessmentPayment,
          totalPayment: claim.mileagePayment + claim.assessmentPayment
        });
    });

    const columnHeaders = [
      {cell: 'A1', title: 'Claim Reference'},
      {cell: 'B1', title: 'Claim Status'},
      {cell: 'C1', title: 'Assessment Date'},
      {cell: 'D1', title: 'Assessment Postcode'},
      {cell: 'E1', title: 'Successful Assessment'},
      {cell: 'F1', title: 'Miles Travelled'},
      {cell: 'G1', title: 'Mileage Payment'},
      {cell: 'H1', title: 'Assessment Payment'},
      {cell: 'I1', title: 'Claim Total'},
    ];

    this.excelService
    .exportAsExcelFile(this.exportData, 'FMAS12D_ClaimExport', columnHeaders)
    .subscribe(result => {
      this.toastService.displaySuccess({
        title: 'Export Successful',
        message: 'Claims export file created'
      });
    }, err => {
      this.toastService.displayError({
        title: 'Error',
        message: 'Error creating claims export file !'
      });
    });

  }

  onSort({column, direction, columnType}: SortEvent) {
    this.headers.forEach(header => {
      if (header.sortable !== column) {
        header.direction = '';
      }

      this.claimsService.sortColumn = column;
      this.claimsService.sortDirection = direction;
      this.claimsService.sortColumnType = columnType === undefined ? 'string' : columnType;
    });
  }

}
