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

  createCcgExportEntry(claim): InvoicePaymentFile {
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
    const exportData = (claimsForCcg.map(this.createCcgExportEntry));

    const columnHeaders = [
      {cell: 'A1', title: 'Transaction Description'},
      {cell: 'B1', title: 'Vendor Code'},
      {cell: 'C1', title: 'Invoice Number'},
      {cell: 'D1', title: 'Invoice Date'},
      {cell: 'E1', title: 'Invoice Received Date'},
      {cell: 'F1', title: 'Payment Terms'},
      {cell: 'G1', title: 'Transaction Type'},
      {cell: 'H1', title: 'Cost Centre'},
      {cell: 'I1', title: 'Subjective'},
      {cell: 'J1', title: 'Analysis 1'},
      {cell: 'K1', title: 'Analysis 2'},
      {cell: 'L1', title: 'Analysis 3'},
      {cell: 'M1', title: 'Item Description'},
      {cell: 'N1', title: 'Item Type'},
      {cell: 'O1', title: 'Line Amount'},
      {cell: 'P1', title: 'Unit Amount'},
      {cell: 'Q1', title: 'Tax Code'},
      {cell: 'R1', title: 'Line Valid'},
    ];

    if (exportData.length > 0) {

      this.excelService
        .exportAsCcgExcelFile(exportData, ccg.shortCode, ccg.name, columnHeaders)
        .subscribe(result => {
          const claimIds = this.processedClaimIds.concat(exportData.map(claim => claim.id));
          this.updateClaimsService.bulkUpdateClaimStatusToApproved(claimIds)
          .subscribe(x => {
            console.log(x);
          });

          this.toastService.displaySuccess({
            title: 'Success',
            message: `Export file created for ${result}`
          });
        }, err => {
          this.toastService.displayError({
            title: 'Error',
            message: err
          });
        });
    }
    return exportData.length;
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
