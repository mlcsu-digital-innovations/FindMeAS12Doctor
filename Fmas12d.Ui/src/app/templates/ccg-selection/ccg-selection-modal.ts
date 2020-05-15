import { Component, Input, Output, EventEmitter } from '@angular/core';
import { SelectableCcg } from 'src/app/interfaces/selectableCcg';

@Component({
  selector: 'app-ccg-selection-modal',
  templateUrl: './ccg-selection-modal.html',
  styleUrls: ['./ccg-selection-modal.css']
})
export class CcgSelectionModalComponent {

  allSelected = false;

  @Input()
  selectedCcgs: SelectableCcg[];

  @Output() actioned = new EventEmitter<boolean>();

  modalAction(action: boolean) {
    this.actioned.emit(action);
  }

  ToggleSelection(index: number) {
    this.selectedCcgs[index].selected = !this.selectedCcgs[index].selected;
  }

  ToggleAllSelection() {
    this.allSelected = !this.allSelected;

    this.selectedCcgs.forEach(ccg => {
      ccg.selected = this.allSelected;
    });
  }

}
