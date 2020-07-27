import { NgModule } from '@angular/core';
import { ContactNumberPipe } from '../pipes/contact-number.pipe';


@NgModule({
  imports: [
  ],
  declarations: [ContactNumberPipe],
  exports: [
    ContactNumberPipe
  ]
})
export class SharedModule {}
