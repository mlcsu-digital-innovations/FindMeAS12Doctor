import { CommonModule } from '@angular/common';
import { DisableControlDirective } from '../directives/disable-control/disable-control.directive';
import { NavbarComponent } from './navbar/navbar.component';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { TemplateModule } from '../templates/template.module';
import { ToastsComponent } from './toasts/toasts.component';

@NgModule({
  declarations: [
    DisableControlDirective,
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
    ReactiveFormsModule,
    NgbModule,
    NavbarComponent,
    DisableControlDirective,
    TemplateModule,
    ToastsComponent
  ],
  providers: []
})
export class SharedComponentsModule {}
