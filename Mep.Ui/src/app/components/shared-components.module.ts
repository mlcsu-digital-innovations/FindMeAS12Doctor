
import { CommonModule } from '@angular/common';
import { DelaySpinnerComponent } from '../components/delay-spinner/delay-spinner.component';
import { DisableControlDirective } from '../directives/disable-control/disable-control.directive';
import { FormsModule } from '@angular/forms';
import { FocusOnShowDirective } from '../directives/focus-on-show/focus-on-show.directive';
import { NavbarComponent } from './navbar/navbar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TemplateModule } from '../templates/template.module';
import { ToastsComponent } from './toasts/toasts.component';
import { TableHeaderSortable } from '../directives/table-header-sortable/table-header-sortable.directive';

@NgModule({
  declarations: [
    DelaySpinnerComponent,
    DisableControlDirective,    
    FocusOnShowDirective,
    NavbarComponent,
    TableHeaderSortable,
    ToastsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
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
    NgbModule,
    ReactiveFormsModule,
    TableHeaderSortable,
    TemplateModule,
    ToastsComponent
  ],
  providers: []
})
export class SharedComponentsModule {}
