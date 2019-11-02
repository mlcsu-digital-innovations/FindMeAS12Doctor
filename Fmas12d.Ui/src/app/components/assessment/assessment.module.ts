import { CommonModule } from '@angular/common';
import { ExaminationCreateComponent } from './assessment-create/assessment-create.component';
import { ExaminationEditComponent } from './assessment-edit/assessment-edit.component';
import { ExaminationListComponent } from './assessment-list/assessment-list.component';
import { ExaminationRoutes } from './assessment.routes';
import { ExaminationViewComponent } from './examination-view/examination-view.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedComponentsModule } from '../shared-components.module';

@NgModule({
  declarations: [
    ExaminationCreateComponent,
    ExaminationEditComponent,
    ExaminationListComponent,
    ExaminationViewComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ExaminationRoutes),
    SharedComponentsModule
  ],
  providers: []
})
export class ExaminationModule {}
