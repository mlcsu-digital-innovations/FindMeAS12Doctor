import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FinanceClaim } from '../../interfaces/finance-claim';
import { Ccg } from 'src/app/interfaces/ccg';


@Component({
  selector: 'app-finance-export-modal',
  templateUrl: './finance-export-modal.html',
  styleUrls: ['./finance-export-modal.css']
})
export class FinanceExportModalComponent implements OnInit {

  @Input() exportClaims: FinanceClaim[];

  @Output() actioned = new EventEmitter<boolean>();

  exportingCcgs: Ccg[] = [];
  previouslyExported: number;

  modalAction(action: boolean) {
    this.actioned.emit(action);
  }

  ngOnInit() {
    this.exportingCcgs =
      this.exportClaims.map((x: FinanceClaim) => x.ccg)
      .filter((ccg, i, arr) => arr.findIndex(t => t.id === ccg.id) === i);

    const claims =
      this.exportClaims.filter(x => x.exportedDate !== null);

    this.previouslyExported = claims.length;
  }
}
