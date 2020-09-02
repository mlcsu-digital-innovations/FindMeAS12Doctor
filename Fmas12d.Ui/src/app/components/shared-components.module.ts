import { AutoLoginComponent } from './auto-login/auto-login.component';
import { CommonModule } from '@angular/common';
import { CustomPipe } from 'src/app/pipes/custom-pipe.module';
import { DelaySpinnerComponent } from '../components/delay-spinner/delay-spinner.component';
import { DisableControlDirective } from '../directives/disable-control/disable-control.directive';
import { FocusOnShowDirective } from '../directives/focus-on-show/focus-on-show.directive';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgbDateCustomParserFormatter } from '../components/datePicker-format/datePicker-format';
import { NgbModule, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TableHeaderSortable } from '../directives/table-header-sortable/table-header-sortable.directive';
import { TemplateModule } from '../templates/template.module';
import { ToastsComponent } from './toasts/toasts.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { ListFilterComponent } from 'src/app/components/list-filter/list-filter.component';
import { SignOutComponent } from './sign-out/signout.component';
import { AboutComponent } from './about/about.component';

@NgModule({
  declarations: [
    AboutComponent,
    AutoLoginComponent,
    DelaySpinnerComponent,
    DisableControlDirective,
    FocusOnShowDirective,
    ListFilterComponent,
    NavbarComponent,
    TableHeaderSortable,
    ToastsComponent,
    UnauthorizedComponent,
    UserProfileComponent,
    WelcomeComponent,
    SignOutComponent
  ],
  imports: [
    CommonModule,
    CustomPipe,
    FormsModule,
    NgMultiSelectDropDownModule,
    NgbModule,
    ReactiveFormsModule,
    RouterModule,
    TemplateModule
  ],
  exports: [
    CommonModule,
    CustomPipe,
    DelaySpinnerComponent,
    DisableControlDirective,
    FocusOnShowDirective,
    FormsModule,
    ListFilterComponent,
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
export class SharedComponentsModule {
  public static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedComponentsModule,
      providers: [
        {
          provide: NgbDateParserFormatter, useClass: NgbDateCustomParserFormatter
        }
      ]
    };
  }
}
