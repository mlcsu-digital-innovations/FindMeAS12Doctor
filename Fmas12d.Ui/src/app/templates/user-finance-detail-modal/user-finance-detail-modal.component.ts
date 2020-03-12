import { BankDetailsProfile } from 'src/app/interfaces/bank-details-profile';
import { Ccg } from 'src/app/interfaces/ccg';
import { CcgListService } from 'src/app/services/ccg-list/ccg-list.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { ToastService } from 'src/app/services/toast/toast.service';
import { debounceTime, distinctUntilChanged, tap, switchMap, catchError } from 'rxjs/operators';

@Component({
  selector: 'app-user-finance-detail-modal',
  templateUrl: './user-finance-detail-modal.component.html',
  styleUrls: ['./user-finance-detail-modal.component.css']
})
export class UserFinanceDetailModalComponent implements OnInit {
  @Input() public financeDetail: BankDetailsProfile;
  @Input() financeDetails: BankDetailsProfile[];
  @Output() actioned = new EventEmitter<any>();

  financeDetailForm: FormGroup;  
  hasCcgSearchFailed: boolean;
  isCcgSearching: boolean;
  unknownCcgId: number;

  constructor(
    private ccgListService: CcgListService,
    private formBuilder: FormBuilder,
    private toastService: ToastService
  ) { }

  ngOnInit() {
    this.financeDetailForm = this.formBuilder.group({
      ccg: ['', Validators.required],      
      vsrNumber: ['', Validators.required]      
    });

    if (this.financeDetail) {               
      let selectedFinanceDetail: BankDetailsProfile = this.financeDetails
        .find(item => item.ccgId === this.financeDetail.ccgId);
      
      if (selectedFinanceDetail) {
        this.financeDetail = selectedFinanceDetail;
      }
      
      this.controls.ccg.setValue({ 
        id: this.financeDetail.ccgId, 
        resultText: this.financeDetail.ccg.name 
      });
      this.controls.vsrNumber.setValue(this.financeDetail.vsrNumber);
      this.controls.ccg.disable();
    }
  }

  get controls() {
    return this.financeDetailForm.controls;
  }  

  CcgSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isCcgSearching = true)),
      switchMap(term =>
        this.ccgListService.GetCcgList(term).pipe(
          tap(() => (this.hasCcgSearchFailed = false)),
          tap((results: any[]) => (
            this.ValidateTypeAheadResults(results, 'ccg')
          )),
          catchError(() => {
            this.hasCcgSearchFailed = true;
            this.toastService.displayError({ message: 'Error searching for CCGs'});
            return of([]);
          })
        )
      ),
      tap(() => (this.isCcgSearching = false))
    )

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  Cancel() {
    this.actioned.emit(null);
  }

  SaveFinanceDetail() {
    if (this.financeDetailForm.valid) {               
      const bankDetail = {} as BankDetailsProfile;
      bankDetail.ccgId = this.controls.ccg.value.id;
      bankDetail.ccg = {} as Ccg;
      bankDetail.ccg.id = bankDetail.ccgId;
      bankDetail.ccg.name = this.controls.ccg.value.resultText;
      bankDetail.vsrNumber = this.controls.vsrNumber.value;

      if (this.financeDetail) {
        bankDetail.id = this.financeDetail.id;
      }

      let ccgAlreadyInFinanceDetails = this.financeDetails 
        ? this.financeDetails.find(item => item.ccgId == this.controls.ccg.value.id) 
        : null;
      if (ccgAlreadyInFinanceDetails && bankDetail.id !== ccgAlreadyInFinanceDetails.id) {
        this.controls.ccg.setErrors({ uniqueCCGsRequired: true });
      }
      else {
        this.actioned.emit(bankDetail);
      }
    }    
  }

  ValidateTypeAheadResults(results: any[], fieldName: string) {
    if (results == null) {
      this.controls[fieldName].setErrors({ NoMatchingResults: true });
    }
  }
}
