import { Component, QueryList, ViewChildren, OnInit, PipeTransform } from '@angular/core';
import { DoctorClaimListService } from 'src/app/services/doctor-claim-list/doctor-claim-list.service';
import { Observable } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { TableHeaderSortable, SortEvent } from '../../../directives/table-header-sortable/table-header-sortable.directive';
import { ToastService } from '../../../services/toast/toast.service';
import { UserAssessmentClaim } from 'src/app/interfaces/user-assessment-claim';
import { ExcelService } from 'src/app/services/excel-service/excel.service';
import { DoctorClaimExport } from 'src/app/interfaces/doctor-claim-export';
import * as moment from 'moment';

@Component({
  selector: 'app-doctor-claim-list',
  styleUrls: ['./doctor-claim-list.component.css'],
  templateUrl: './doctor-claim-list.component.html'
})
export class DoctorClaimListComponent implements OnInit {

  error: any;
  claimsList$: Observable<UserAssessmentClaim[]>;
  total$: Observable<number>;

  displayedList: UserAssessmentClaim[];
  exportData: DoctorClaimExport[];

  @ViewChildren(TableHeaderSortable) headers: QueryList<TableHeaderSortable>;

  constructor(
    public oidcSecurityService: OidcSecurityService,
    private claimsService: DoctorClaimListService,
    private excelService: ExcelService,
    private toastService: ToastService,
  ) {
  }

  ngOnInit() {

    this.claimsList$ = this.claimsService.getClaims(true);
    this.total$ = this.claimsService.total$;

    this.claimsList$.subscribe(
      result => {
        console.log(result);
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
    // export: DoctorClaimExport[];

    this.exportData = [];

    console.log('exporting claims');



    this.displayedList.forEach(claim => {

    console.log(moment(claim.assessment.scheduledTime).toISOString());

    this.exportData.push({
        claimReference: claim.claimReference,
        claimStatus: claim.claimStatus as string,
        assessmentDate: moment(claim.assessment.scheduledTime).toISOString(),
        assessmentPostcode: claim.assessment.postcode,
        successfulAssessment: claim.assessment.isSuccessful,
        mileage: claim.mileage,
        mileagePayment: claim.mileagePayment,
        assessmentPayment: claim.assessmentPayment,
        totalPayment: claim.mileagePayment + claim.assessmentPayment
      });
    });

    this.excelService.exportAsExcelFile(this.exportData, 'FMAS12D_ClaimExport');

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
