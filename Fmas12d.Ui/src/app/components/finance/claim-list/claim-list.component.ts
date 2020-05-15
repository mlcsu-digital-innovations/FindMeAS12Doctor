import { Component, QueryList, ViewChildren, OnInit, ViewChild } from '@angular/core';
import { FinanceClaim } from 'src/app/interfaces/finance-claim';
import { FinanceClaimListService } from 'src/app/services/finance-claim-list/finance-claim-list.service';
import { Observable } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { TableHeaderSortable, SortEvent } from '../../../directives/table-header-sortable/table-header-sortable.directive';
import { ToastService } from '../../../services/toast/toast.service';
import { SelectableCcg } from 'src/app/interfaces/selectableCcg';
import { ExcelService } from 'src/app/services/excel-service/excel.service';
import { CcgClaimExport } from 'src/app/interfaces/ccg-claim-export';
import * as moment from 'moment';
import { CLAIM_STATUS_PROCESSING } from 'src/app/constants/Constants';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FinanceClaimService } from 'src/app/services/finance-claim/finance-claim.service';
import { InvoicePaymentFile } from 'src/app/interfaces/InvoicePaymentFile';
import { MedExamLogA } from 'src/app/interfaces/med-exam-log';
import { BankDetails } from 'src/app/interfaces/bank-details';

@Component({
  selector: 'app-claim-list',
  styleUrls: ['./claim-list.component.css'],
  templateUrl: './claim-list.component.html'
})
export class ClaimListComponent implements OnInit {

  ccgModal: NgbModalRef;
  claimsList$: Observable<FinanceClaim[]>;
  error: any;
  total$: Observable<number>;
  availableCcgs: SelectableCcg[] = [];
  hasVisibleData: boolean;
  activeClaims: FinanceClaim[] = [];
  processedClaimIds: number[] = [];

  @ViewChildren(TableHeaderSortable) headers: QueryList<TableHeaderSortable>;
  @ViewChild('selectCcg', { static: true }) ccgSelectionTemplate;

  constructor(
    private excelService: ExcelService,
    public claimsService: FinanceClaimListService,
    private updateClaimsService: FinanceClaimService,
    private modalService: NgbModal,
    private toastService: ToastService,
    public oidcSecurityService: OidcSecurityService,
  ) {
  }

  ngOnInit() {
    this.getData(true);
  }

  getData(refresh: boolean = false) {
    if (refresh || ! this.claimsList$) {
      this.claimsList$ = this.claimsService.getClaims(refresh);
      this.total$ = this.claimsService.total$;

      this.claimsList$.subscribe(
        result => {
          this.availableCcgs =
            result.map(this.getCcgFromClaim)
            .filter((ccg, i, arr) => arr.findIndex(t => t.id === ccg.id) === i);

          this.hasVisibleData = result.length > 0;
          this.activeClaims = result;
        },
        error => {
          this.toastService.displayError({
            title: 'Error',
            message: error
          });
        }
      );
    }
  }

  createCcgExportEntryForInvoicePaymentFile(claim: FinanceClaim): InvoicePaymentFile {

    return {
      TransactionDescription: '',
      VendorCode: '',
      InvoiceNumber: '',
      InvoiceDate: moment().toDate(),
      InvoiceReceivedDate: moment().toDate(),
      PaymentTerms: '7 DAYS NET',
      TransactionType: 'STANDARD',
      CostCentre: claim.ccg.costCentre,
      Subjective: '',
      Analysis1: '00000',
      Analysis2: '00000',
      Analysis3: '00000',
      ItemDescription: '',
      ItemType: 'ITEM',
      LineAmount: claim.mileagePayment + claim.assessmentPayment,
      UnitAmount: 1.00,
      TaxCode: 'EXEMPT',
      LineValid: 'VALID',
      id: claim.id
    };
  }

  createCcgExportForMedExamLogFile(claim: FinanceClaim): MedExamLogA {

    claim.claimant.bankDetails.forEach(detail => {
      console.log(detail);
    });

    const bankDetail: BankDetails = claim.claimant.bankDetails.find(detail => detail.ccgId === claim.ccg.id);

    return {
      dateLogged: null,
      lastActionDate: claim.lastUpdated,
      ccgCode: claim.ccg.shortCode,
      doctorName: claim.claimant.displayName,
      vsrNUmber: bankDetail.vsrNumber,
      dateOfExam: claim.assessment.scheduledTime,
      patientIdentifer: '',
      dateReceived: null,
      value: claim.assessmentPayment,
      mileage: claim.mileagePayment,
      total: claim.assessmentPayment + claim.mileagePayment,
      loggedBy: '',
      status: '',
      ipfTransactionDescription: '',
      invoiceNumber: '',
      payRef: null,
      ipfFile: '',
      notes: ''
    };
  }

  exportCcgClaims(ccg: SelectableCcg): number {

    const claimsForCcg = this.activeClaims
      .filter(claim => claim.ccg.id === ccg.id && claim.claimStatus.id === CLAIM_STATUS_PROCESSING);
    const exportDataForIPF = (claimsForCcg.map(this.createCcgExportEntryForInvoicePaymentFile));
    const exportDataForMedExamLog = (claimsForCcg.map(this.createCcgExportForMedExamLogFile));

    // ToDo: confirm format of files, populate missing fields

    // Medical Examination Log Export
    if (exportDataForMedExamLog.length > 0) {
      this.excelService
      .createMedExamLogExport(exportDataForMedExamLog, ccg.shortCode, ccg.name)
      .subscribe(result => {

        this.toastService.displaySuccess({
                title: 'Success',
                message: `Med Exam Log file created for ${result}`
              });
      });
    }

    // MHA Batch Update files
    if (exportDataForIPF.length > 0) {
      this.excelService
        .createMhaBatchExport(exportDataForIPF, ccg.shortCode, ccg.name)
        .subscribe(result => {

          const claimIds = this.processedClaimIds.concat(exportDataForIPF.map(claim => claim.id));
          this.updateClaimsService.bulkUpdateClaimStatusToApproved(claimIds)
            .subscribe(x => {
              console.log(x);
          });

          this.toastService.displaySuccess({
                  title: 'Success',
                  message: `Export file created for ${result}`
                });
        });
    }
    return exportDataForIPF.length;
  }

  exportCcgData() {
    this.ccgModal = this.modalService.open(this.ccgSelectionTemplate, {
      size: 'lg'
    });
  }

  getCcgFromClaim(claim: FinanceClaim) {
    return {id: claim.ccg.id, name: claim.ccg.name, shortCode: claim.ccg.shortCode, selected: true};
  }

  OnCcgSelection(action: boolean) {
    this.ccgModal.close();
    if (action) {
      const exportCcgs = this.availableCcgs.filter(ccg => ccg.selected === true);

      exportCcgs.forEach(ccg => {
        if (this.exportCcgClaims(ccg) === 0) {
          this.toastService.displayInfo({
            title: 'Information',
            message: `No claims exported for ${ccg.name}`
          });
        }
      });

      setTimeout(() => this.getData(true), 2000);
    }
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
