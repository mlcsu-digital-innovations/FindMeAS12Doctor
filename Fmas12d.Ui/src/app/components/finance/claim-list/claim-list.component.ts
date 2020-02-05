import { Component, QueryList, ViewChildren, OnInit, PipeTransform } from '@angular/core';
import { Observable } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { TableHeaderSortable, SortEvent } from '../../../directives/table-header-sortable/table-header-sortable.directive';
import { ToastService } from '../../../services/toast/toast.service';
import { FinanceClaimListService } from 'src/app/services/finance-claim-list/finance-claim-list.service';
import { FinanceClaim } from 'src/app/interfaces/finance-claim';

@Component({
  selector: 'app-claim-list',
  styleUrls: ['./claim-list.component.css'],
  templateUrl: './claim-list.component.html'
})
export class ClaimListComponent implements OnInit {

  error: any;
  claimsList$: Observable<FinanceClaim[]>;
  total$: Observable<number>;

  @ViewChildren(TableHeaderSortable) headers: QueryList<TableHeaderSortable>;

  constructor(
    public oidcSecurityService: OidcSecurityService,
    private claimsService: FinanceClaimListService,
    private toastService: ToastService,
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


}
