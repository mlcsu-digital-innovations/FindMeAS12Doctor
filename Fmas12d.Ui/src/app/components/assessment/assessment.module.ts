import { AssessmentCreateComponent } from './assessment-create/assessment-create.component';
import { AssessmentEditComponent } from './assessment-edit/assessment-edit.component';
import { AssessmentListComponent } from './assessment-list/assessment-list.component';
import { AssessmentOutcomeComponent } from './assessment-outcome/assessment-outcome.component';
import { AssessmentRoutes } from './assessment.routes';
import { AssessmentViewComponent } from './assessment-view/assessment-view.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedComponentsModule } from '../shared-components.module';

@NgModule({
  declarations: [
    AssessmentCreateComponent,
    AssessmentEditComponent,
    AssessmentListComponent,
    AssessmentOutcomeComponent,
    AssessmentViewComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(AssessmentRoutes),
    SharedComponentsModule
  ],
  providers: []
})
export class AssessmentModule {}
