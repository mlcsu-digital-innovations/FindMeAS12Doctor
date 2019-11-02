import { CommonModule } from '@angular/common';
import { AssessmentCreateComponent } from './assessment-create/assessment-create.component';
import { AssessmentEditComponent } from './assessment-edit/assessment-edit.component';
import { AssessmentListComponent } from './assessment-list/assessment-list.component';
import { AssessmentRoutes } from './assessment.routes';
import { AssessmentViewComponent } from './assessment-view/assessment-view.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedComponentsModule } from '../shared-components.module';

@NgModule({
  declarations: [
    AssessmentCreateComponent,
    AssessmentEditComponent,
    AssessmentListComponent,
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
