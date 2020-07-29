import { CommonModule } from '@angular/common';
import { DateTimeFormatPipe } from 'src/app/pipes/dateTimeFormat-pipe';
import { NgModule } from '@angular/core';
import { TimeOnlyFormatPipe } from './timeOnlyFormat-pipe';
import { ContactNumberPipe } from './contact-number.pipe';

@NgModule({
  declarations: [
    ContactNumberPipe,
    DateTimeFormatPipe,
    TimeOnlyFormatPipe
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ContactNumberPipe,
    DateTimeFormatPipe,
    TimeOnlyFormatPipe
  ]
})

export class CustomPipe{}
