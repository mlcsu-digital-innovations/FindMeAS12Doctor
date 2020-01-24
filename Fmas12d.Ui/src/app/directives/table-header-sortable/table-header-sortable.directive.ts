import { Directive, EventEmitter, Input, Output } from '@angular/core';

export type SortDirection = 'asc' | 'desc' | '';
const rotate: { [key: string]: SortDirection } = { 'asc': 'desc', 'desc': '', '': 'asc' };

export interface SortEvent {
  column: string;
  direction: SortDirection;
  columnType: string;
}

@Directive({
  host: {
    '[class.asc]': 'direction === "asc"',
    '[class.desc]': 'direction === "desc"',
    '(click)': 'rotate()'
  },
  selector: 'th[sortable]'
})
export class TableHeaderSortable {

  @Input() direction: SortDirection = '';
  @Input() sortable: string;
  @Input() columnType: string;
  @Output() sort = new EventEmitter<SortEvent>();

  rotate() {
    this.direction = rotate[this.direction];
    this.sort.emit({ column: this.sortable, direction: this.direction, columnType: this.columnType });
  }
}