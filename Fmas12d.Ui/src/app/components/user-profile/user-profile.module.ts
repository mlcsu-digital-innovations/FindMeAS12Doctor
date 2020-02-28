import { CommonModule, DecimalPipe } from '@angular/common';
import { DoctorClaimListComponent } from './doctor-claims-list/doctor-claim-list.component';
import { DoctorClaimViewComponent } from './doctor-claim-view/doctor-claim-view.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedComponentsModule } from '../shared-components.module';
import { UserProfileRoutes } from './user-profile.routes';

@NgModule({
  declarations: [
    DoctorClaimListComponent,
    DoctorClaimViewComponent
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
