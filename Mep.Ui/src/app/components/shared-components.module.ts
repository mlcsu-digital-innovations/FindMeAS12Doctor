import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { DisableControlDirective } from '../directives/disable-control/disable-control.directive';
import { NavbarComponent } from './navbar/navbar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastsComponent } from './toasts/toasts.component';

@NgModule({
  declarations: [
    NavbarComponent,
    DisableControlDirective,
    ToastsComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule,
    NavbarComponent,
    DisableControlDirective,
    ToastsComponent
  ],
  providers: []
})
export class SharedComponentsModule {}
