import { CcgClaimExport } from 'src/app/interfaces/ccg-claim-export';
import { CLAIM_STATUS_PROCESSING } from 'src/app/constants/Constants';
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

  exportCcgClaims(ccg: SelectableCcg): number {

    const claimsForCcg = this.activeClaims
      .filter(claim => claim.ccg.id === ccg.id && claim.claimStatus.id === CLAIM_STATUS_PROCESSING);
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

          if (ccg.requiresApproval) {
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

  exportCcgData() {
    this.ccgModal = this.modalService.open(this.ccgSelectionTemplate, {
      size: 'lg'
    });
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
