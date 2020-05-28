import { CcgClaimExport } from 'src/app/interfaces/ccg-claim-export';
import { CLAIM_STATUS_PROCESSING, CLAIM_STATUS_QUERY } from 'src/app/constants/Constants';
import { Component, QueryList, ViewChildren, OnInit, ViewChild } from '@angular/core';
import { ExcelService } from 'src/app/services/excel-service/excel.service';
import { FinanceClaim } from 'src/app/interfaces/finance-claim';
import { FinanceClaimListService } from 'src/app/services/finance-claim-list/finance-claim-list.service';
import { FinanceClaimService } from 'src/app/services/finance-claim/finance-claim.service';
import { InvoicePaymentFile } from 'src/app/interfaces/InvoicePaymentFile';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { SelectableCcg } from 'src/app/interfaces/selectableCcg';
import { TableHeaderSortable, SortEvent } from '../../../directives/table-header-sortable/table-header-sortable.directive';
import { ToastService } from '../../../services/toast/toast.service';
import * as moment from 'moment';
import { FilterItem } from 'src/app/interfaces/filterItem';
import { Ccg } from 'src/app/interfaces/ccg';

@Component({
  selector: 'app-claim-list',
  styleUrls: ['./claim-list.component.css'],
  templateUrl: './claim-list.component.html'
})
export class ClaimListComponent implements OnInit {

  confirmationModal: NgbModalRef;
  claimsList$: Observable<FinanceClaim[]>;
  error: any;
  total$: Observable<number>;
  availableCcgs: SelectableCcg[] = [];
  hasVisibleData: boolean;
  activeClaims: FinanceClaim[] = [];
  processedClaimIds: number[] = [];

  activeStatuses: FilterItem[] = [];

  @ViewChildren(TableHeaderSortable) headers: QueryList<TableHeaderSortable>;
  @ViewChild('confirmExport', { static: true }) confirmExportTemplate;

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

    const activeAccount =
      claim.claimant.bankDetails.filter(account => account.ccgId === claim.ccg.id);

    const vsrNumber = activeAccount.length > 0 ? activeAccount[0].vsrNumber.toString() : '';

    const assessmentDate = moment(claim.assessment.completedTime).format('DD-MM-YYYY');
    const invoiceDate = moment(claim.assessment.completedTime).format('DDMMM').toUpperCase();
    const postcode = claim.assessment.postcode.replace(' ', '');
    const incode = postcode.substr(postcode.length - 3, 3);

    const transactionDescription = `MHA/${incode}/${assessmentDate}`;

    const invoiceNumber = `${incode}${invoiceDate}${claim.claimReference}`;


    // Analysis codes should NOT be changed !
    return {
      TransactionDescription: transactionDescription,
      VendorCode: vsrNumber,
      InvoiceNumber: invoiceNumber,
      InvoiceDate: moment().toDate(),
      InvoiceReceivedDate: moment().toDate(),
      PaymentTerms: '7 DAYS NET',
      TransactionType: 'STANDARD',
      CostCentre: claim.ccg.costCentre,
      Subjective: claim.ccg.subjectiveCode,
      Analysis1: '00000',
      Analysis2: '000000',
      Analysis3: '000000',
      ItemDescription: '',
      ItemType: 'ITEM',
      LineAmount: claim.mileagePayment + claim.assessmentPayment,
      UnitAmount: 1.00,
      TaxCode: 'EXEMPT',
      LineValid: 'VALID',
      id: claim.id
    };
  }

  exportCcgClaims(ccg: Ccg): number {

    const claimsForCcg = this.activeClaims
      .filter(claim => claim.ccg.id === ccg.id && claim.claimStatus.id !== CLAIM_STATUS_QUERY);

    const exportDataForIPF = (claimsForCcg.map(this.createCcgExportEntryForInvoicePaymentFile));

    // ToDo: confirm format of files, populate missing fields

    // MHA Batch Update files
    if (exportDataForIPF.length > 0) {
      this.excelService
        .createMhaBatchExport(exportDataForIPF, ccg.shortCode, ccg.name)
        .subscribe(result => {

          const claimIds = this.processedClaimIds.concat(
            claimsForCcg.map(claim => claim.id)
          );

          if (ccg.isPaymentApprovalRequired) {
            this.updateClaimsService.bulkUpdateClaimStatusToAwaitingCcgApproval(claimIds)
            .subscribe(x => {
              console.log(`${x} claims updated to awaiting approval`);
            });
          } else {
            this.updateClaimsService.bulkUpdateClaimStatusToApproved(claimIds)
            .subscribe(x => {
              console.log(`${x} claims updated to approved`);
          });
          }

          this.toastService.displaySuccess({
                  title: 'Success',
                  message: `Export file created for ${result}`
                });
        });
    }
    return exportDataForIPF.length;
  }

  exportData() {
    this.confirmationModal = this.modalService.open(this.confirmExportTemplate, {
      size: 'lg'
    });
  }

  getClaimStatus(claim: FinanceClaim): FilterItem {
    return {
      id: claim.claimStatus.id,
      name: claim.claimStatus.name,
      selected: true
    };
  }

  getCcgFromClaim(claim: FinanceClaim) {
    return {
      id: claim.ccg.id,
      name: claim.ccg.name,
      shortCode: claim.ccg.shortCode,
      selected: true,
      requiresApproval: claim.ccg.isPaymentApprovalRequired
    };
  }

  // OnCcgSelection(action: boolean) {
  //   this.ccgModal.close();
  //   if (action) {
  //     const exportCcgs = this.availableCcgs.filter(ccg => ccg.selected === true);

  //     exportCcgs.forEach(ccg => {
  //       if (this.exportCcgClaims(ccg) === 0) {
  //         this.toastService.displayInfo({
  //           title: 'Information',
  //           message: `No claims exported for ${ccg.name}`
  //         });
  //       }
  //     });

  //     setTimeout(() => this.getData(true), 2000);
  //   }
  // }

  OnExportConfirmed(action: boolean) {
    this.confirmationModal.close();
    if (action) {

      const exportCcgs =
        this.activeClaims.map((x: FinanceClaim) => x.ccg)
        .filter((ccg, i, arr) => arr.findIndex(t => t.id === ccg.id) === i);

      console.log(exportCcgs);

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
