
import { CommonModule } from '@angular/common';
import { DelaySpinnerComponent } from '../components/delay-spinner/delay-spinner.component';
import { DisableControlDirective } from '../directives/disable-control/disable-control.directive';
import { FocusOnShowDirective } from '../directives/focus-on-show/focus-on-show.directive';
import { NavbarComponent } from './navbar/navbar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TemplateModule } from '../templates/template.module';
import { ToastsComponent } from './toasts/toasts.component';

@NgModule({
  declarations: [
    DelaySpinnerComponent,
    DisableControlDirective,
    FocusOnShowDirective,
    NavbarComponent,
    ToastsComponent
  ],
  imports: [
    CommonModule,
    NgbModule,
    ReactiveFormsModule,
    TemplateModule
  ],
  exports: [
    CommonModule,
    DelaySpinnerComponent,
    DisableControlDirective,
    FocusOnShowDirective,
    NavbarComponent,
    NgbModule,
    ReactiveFormsModule,
    TemplateModule,
    ToastsComponent
  ],
  providers: []
})
export class SharedComponentsModule {}
