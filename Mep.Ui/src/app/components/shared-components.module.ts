

import { CommonModule } from '@angular/common';
import { DelaySpinnerComponent } from '../components/delay-spinner/delay-spinner.component';
import { DisableControlDirective } from '../directives/disable-control/disable-control.directive';
import { FocusOnShowDirective } from '../directives/focus-on-show/focus-on-show.directive';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';
import { NgbDateCustomParserFormatter } from '../components/datePicker-format/datePicker-format';
import { NgbModule, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ReactiveFormsModule } from '@angular/forms';
import { TableHeaderSortable } from '../directives/table-header-sortable/table-header-sortable.directive';
import { TemplateModule } from '../templates/template.module';
import { ToastsComponent } from './toasts/toasts.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { WelcomeComponent } from './welcome/welcome.component';

@NgModule({
  declarations: [
    DelaySpinnerComponent,
    DisableControlDirective,
    FocusOnShowDirective,
    NavbarComponent,
    TableHeaderSortable,
    ToastsComponent,
    UnauthorizedComponent,
    WelcomeComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgMultiSelectDropDownModule,
    NgbModule,
    ReactiveFormsModule,
    TemplateModule
  ],
  exports: [
    CommonModule,
    DelaySpinnerComponent,
    DisableControlDirective,
    FocusOnShowDirective,
    FormsModule,
    NavbarComponent,
    NgMultiSelectDropDownModule,
    NgbModule,
    ReactiveFormsModule,
    TableHeaderSortable,
    TemplateModule,
    ToastsComponent
  ],
  providers: [
    {
      provide: NgbDateParserFormatter, useClass: NgbDateCustomParserFormatter
    }
  ]
})
export class SharedComponentsModule {}
