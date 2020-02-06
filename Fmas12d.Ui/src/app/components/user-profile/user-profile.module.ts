import { DoctorClaimListComponent } from './doctor-claims-list/doctor-claim-list.component';
import { CommonModule, DecimalPipe } from '@angular/common';
import { UserProfileRoutes } from './user-profile.routes';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedComponentsModule } from '../shared-components.module';
// import { ClaimViewComponent } from './claim-view/claim-view.component';

@NgModule({
  declarations: [
    DoctorClaimListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(UserProfileRoutes),
    SharedComponentsModule
  ],
  providers: [
    DecimalPipe
  ]
})
export class UserProfileModule {}
