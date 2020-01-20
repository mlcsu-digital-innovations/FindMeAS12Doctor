import { AutoLoginComponent } from './auto-login/auto-login.component';
import { CommonModule } from '@angular/common';
import { CustomPipe } from 'src/app/pipes/custom-pipe.module';
import { DelaySpinnerComponent } from '../components/delay-spinner/delay-spinner.component';
import { DisableControlDirective } from '../directives/disable-control/disable-control.directive';
import { FocusOnShowDirective } from '../directives/focus-on-show/focus-on-show.directive';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';
import { NgModule } from '@angular/core';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgbDateCustomParserFormatter } from '../components/datePicker-format/datePicker-format';
import { NgbModule, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { OnCallDoctorListComponent } from './on-call-doctor-list/on-call-doctor-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TableHeaderSortable } from '../directives/table-header-sortable/table-header-sortable.directive';
import { TemplateModule } from '../templates/template.module';
import { ToastsComponent } from './toasts/toasts.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { WelcomeComponent } from './welcome/welcome.component';

@NgModule({
  declarations: [
    AutoLoginComponent,
    DelaySpinnerComponent,
    DisableControlDirective,
    FocusOnShowDirective,
    NavbarComponent,
    OnCallDoctorListComponent,
    TableHeaderSortable,
    ToastsComponent,
    UnauthorizedComponent,
    WelcomeComponent
  ],
  imports: [
    CommonModule,
    CustomPipe,
    FormsModule,
    NgMultiSelectDropDownModule,
    NgbModule,
    ReactiveFormsModule,
    TemplateModule
  ],
  exports: [
    CommonModule,
    CustomPipe,
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
