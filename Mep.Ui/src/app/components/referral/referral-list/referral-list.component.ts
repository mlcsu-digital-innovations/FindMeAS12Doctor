import { Component, QueryList, ViewChildren, ViewChild } from '@angular/core';
import { Observable, throwError } from 'rxjs';
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
export class ReferralListComponent {

  dangerMessage: string;
  error: any;
  noOfReferralsInList: number;
  referralList$: Observable<ReferralList[]>;  
  total$: Observable<number>;

  @ViewChild('dangerTpl', null) dangerTemplate;
  @ViewChildren(TableHeaderSortable) headers: QueryList<TableHeaderSortable>;

  constructor(
    public referralListService: ReferralListService,
    private toastService: ToastService) { 

    this.referralList$ = referralListService.referralList$;
    this.total$ = referralListService.total$;

    this.referralList$.subscribe(
      result => this.noOfReferralsInList = result.length,
      error => {
        this.dangerMessage = error;
        this.toastService.show(this.dangerTemplate, {
          classname: 'bg-danger text-light',
          delay: 10000
        }); 
      }
    )
  }

  onSort({column, direction}: SortEvent) {
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