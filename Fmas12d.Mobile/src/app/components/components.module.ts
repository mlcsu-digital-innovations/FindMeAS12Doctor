import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { NavbarComponent } from './navbar/navbar.component';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [NavbarComponent],
  entryComponents: [NavbarComponent],
  imports: [
    CommonModule,
    IonicModule
  ],
  exports: [NavbarComponent]
})
export class ComponentsModule { }
