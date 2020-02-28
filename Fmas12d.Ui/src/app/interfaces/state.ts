import { SortDirection } from '../directives/table-header-sortable/table-header-sortable.directive';

export interface State {
  page: number;
  pageSize: number;
  searchTerm: string;
  sortColumn: string;
  sortColumnType: string;
  sortDirection: SortDirection;
}
