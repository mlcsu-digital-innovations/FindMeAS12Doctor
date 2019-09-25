import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ExaminationListComponent } from './examination-list/examination-list.component';
import { ExaminationRoutes } from './examination.routes';
import { SharedComponentsModule } from '../shared-components.module';

@NgModule({
  declarations: [
    ExaminationListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ExaminationRoutes),
    SharedComponentsModule
  ],
  providers: []
})
export class ExaminationModule {}
