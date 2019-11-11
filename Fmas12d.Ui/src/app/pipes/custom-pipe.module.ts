import { CommonModule } from '@angular/common';
import { DateTimeFormatPipe } from 'src/app/pipes/dateTimeFormat-pipe';
import { NgModule } from '@angular/core';
import { TimeOnlyFormatPipe } from './timeOnlyFormat-pipe';

@NgModule({
  declarations: [
    DateTimeFormatPipe,
    TimeOnlyFormatPipe
  ],
  imports: [
    CommonModule
  ],
  exports: [
    DateTimeFormatPipe,
    TimeOnlyFormatPipe
  ]
})

export class CustomPipe{}
