import { BankDetailsProfile } from 'src/app/interfaces/bank-details-profile';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserProfile } from 'src/app/interfaces/user-profile';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UserProfileService } from 'src/app/services/user-profile/user-profile.service';
import { Observable, of } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap, switchMap, catchError } from 'rxjs/operators';
import { CcgListService } from 'src/app/services/ccg-list/ccg-list.service';

@Component({
  selector: 'app-user-finance-detail-modal',
  templateUrl: './user-finance-detail-modal.component.html',
  styleUrls: ['./user-finance-detail-modal.component.css']
})
export class UserFinanceDetailModalComponent implements OnInit {
  @Input() public financeDetail: BankDetailsProfile;
  @Output() actioned = new EventEmitter<any>();

  financeDetailForm: FormGroup;  
  hasCcgSearchFailed: boolean;
  isCcgSearching: boolean;
  unknownCcgId: number;
  userProfile: UserProfile;

  constructor(
    private ccgListService: CcgListService,
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    private userProfileService: UserProfileService
  ) { }

  ngOnInit() {
    this.financeDetailForm = this.formBuilder.group({
      ccg: ['', Validators.required],      
      vsrNumber: ['', Validators.required]      
    });

    if (this.financeDetail) {   
      this.userProfileService.GetUser().subscribe((result: UserProfile) => {
        this.userProfile = result;     
        
        let selectedFinanceDetail: BankDetailsProfile = this.userProfile.financeDetails
          .find(item => item.id === this.financeDetail.id);
        
        if (selectedFinanceDetail) {
          this.financeDetail = selectedFinanceDetail;
        }
        
        this.controls.ccg.setValue({ id: this.financeDetail.ccgId, resultText: this.financeDetail.ccgName });
        this.controls.vsrNumber.setValue(this.financeDetail.VsrNumber);
      });
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
      const result = {} as BankDetailsProfile;
      result.ccgId = this.controls.ccg.value.id;
      result.ccgName = this.controls.ccg.value.resultText;
      result.VsrNumber = this.controls.vsrNumber.value;

      if (this.financeDetail) {
        result.id = this.financeDetail.id;
      }

      this.actioned.emit(result);
    }    
  }

  ValidateTypeAheadResults(results: any[], fieldName: string) {
    if (results == null) {
      this.controls[fieldName].setErrors({ NoMatchingResults: true });
    }
  }
}
