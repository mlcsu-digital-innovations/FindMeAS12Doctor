import { CommonModule, DecimalPipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ReferralCreateComponent } from './referral-create/referral-create.component';
import { ReferralEditComponent } from './referral-edit/referral-edit.component';
import { ReferralListComponent } from './referral-list/referral-list.component';
import { ReferralRoutes } from './referral.routes';
import { SharedComponentsModule } from '../shared-components.module';

@NgModule({
  declarations: [
    ReferralCreateComponent,
    ReferralEditComponent,
    ReferralListComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ReferralRoutes),
    SharedComponentsModule
  ],
  providers: [
    DecimalPipe
  ]
})
export class ReferralModule {}
