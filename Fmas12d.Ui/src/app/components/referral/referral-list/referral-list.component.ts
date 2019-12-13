import { CLOSED, ASSESSMENT_SCHEDULED } from 'src/app/constants/Constants';
import { Component, QueryList, ViewChildren, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { ReferralList } from '../../../interfaces/referral-list';
import { ReferralListService } from '../../../services/referral-list/referral-list-service';
import { TableHeaderSortable, SortEvent } from '../../../directives/table-header-sortable/table-header-sortable.directive';
import { ToastService } from '../../../services/toast/toast.service';

@Component({
  providers: [ReferralListService],
  selector: 'app-referral-list',
  styleUrls: ['./referral-list.component.css'],
  templateUrl: './referral-list.component.html'
})
export class ReferralListComponent implements OnInit {

  error: any;
  noOfReferralsInList: number;
  referralList$: Observable<ReferralList[]>;
  total$: Observable<number>;


  @ViewChildren(TableHeaderSortable) headers: QueryList<TableHeaderSortable>;

  constructor(
    public referralListService: ReferralListService,
    public oidcSecurityService: OidcSecurityService,
    private toastService: ToastService) {

  }

  ngOnInit() {

    this.referralList$ = this.referralListService.referralList$;
    this.total$ = this.referralListService.total$;

    this.referralList$.subscribe(
      result => {
        this.noOfReferralsInList = result.length;
      },
      error => {
        this.toastService.displayError({
          title: 'Error',
          message: error
        });
      }
    );
  }

  CanDoctorsBeAllocated(referralStatusId: number): boolean {
    return referralStatusId !== CLOSED &&
          referralStatusId !== ASSESSMENT_SCHEDULED;
  }

  onSort({ column, direction }: SortEvent) {
    // resetting other headers
    this.headers.forEach(header => {
      if (header.sortable !== column) {
        header.direction = '';
      }
    });

    this.referralListService.sortColumn = column;
    this.referralListService.sortDirection = direction;
  }

}
