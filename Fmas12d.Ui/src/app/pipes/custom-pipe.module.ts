import { DateTimeFormatPipe } from 'src/app/pipes/dateTimeFormat-pipe';
import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';

@NgModule({
  declarations: [
    DateTimeFormatPipe
  ],
  imports: [
    CommonModule
  ],
  exports: [
    DateTimeFormatPipe
  ]
})

export class CustomPipe{}
