import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { DisableControlDirective } from '../directives/disable-control/disable-control.directive';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastsComponent } from './toasts/toasts.component';
import { TableHeaderSortable } from '../directives/table-header-sortable/table-header-sortable.directive';

@NgModule({
  declarations: [
    NavbarComponent,
    DisableControlDirective,
    TableHeaderSortable,
    ToastsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule
  ],
  exports: [
    CommonModule,
    DisableControlDirective,
    FormsModule,
    NavbarComponent,
    NgbModule,    
    ReactiveFormsModule,
    TableHeaderSortable,
    ToastsComponent
  ],
  providers: []
})
export class SharedComponentsModule {}
