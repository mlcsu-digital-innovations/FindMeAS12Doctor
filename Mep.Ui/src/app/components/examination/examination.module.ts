import { CommonModule } from '@angular/common';
import { ExaminationCreateComponent } from './examination-create/examination-create.component';
import { ExaminationListComponent } from './examination-list/examination-list.component';
import { ExaminationRoutes } from './examination.routes';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedComponentsModule } from '../shared-components.module';

@NgModule({
  declarations: [
    ExaminationListComponent,
    ExaminationCreateComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ExaminationRoutes),
    SharedComponentsModule
  ],
  providers: []
})
export class ExaminationModule {}
