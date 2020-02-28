import { Component, QueryList, ViewChildren, OnInit } from '@angular/core';
import { FinanceClaim } from 'src/app/interfaces/finance-claim';
import { FinanceClaimListService } from 'src/app/services/finance-claim-list/finance-claim-list.service';
import { Observable } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { TableHeaderSortable, SortEvent } from '../../../directives/table-header-sortable/table-header-sortable.directive';
import { ToastService } from '../../../services/toast/toast.service';

@Component({
  selector: 'app-claim-list',
  styleUrls: ['./claim-list.component.css'],
  templateUrl: './claim-list.component.html'
})
export class ClaimListComponent implements OnInit {

  claimsList$: Observable<FinanceClaim[]>;
  error: any;
  total$: Observable<number>;

  @ViewChildren(TableHeaderSortable) headers: QueryList<TableHeaderSortable>;

  constructor(
    private claimsService: FinanceClaimListService,
    private toastService: ToastService,
    public oidcSecurityService: OidcSecurityService,
  ) {
  }

  ngOnInit() {

    this.claimsList$ = this.claimsService.getClaims(true);
    this.total$ = this.claimsService.total$;

    this.claimsList$.subscribe(
      result => {
      },
      error => {
        this.toastService.displayError({
          title: 'Error',
          message: error
        });
      }
    );

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
