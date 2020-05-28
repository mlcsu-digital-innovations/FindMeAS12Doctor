import { Component, Input } from '@angular/core';
import { FilterItem } from 'src/app/interfaces/filterItem';
import { FinanceClaimListService } from 'src/app/services/finance-claim-list/finance-claim-list.service';

@Component({
  selector: 'app-list-filter',
  templateUrl: './list-filter.component.html',
  styleUrls: ['./list-filter.component.css']
})
export class ListFilterComponent {

  @Input() filterList: FilterItem[];

  @Input() filtering: boolean;

  @Input() service: FinanceClaimListService;

  @Input() title: string;

}
