import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { TIME_ONLY } from 'src/app/constants/Constants';
import * as moment from 'moment';

@Pipe({ name: 'timeOnlyFormat' })
export class TimeOnlyFormatPipe extends DatePipe implements PipeTransform {
  transform(value: any, args?: any): string {
    return moment(value).format(TIME_ONLY);
  }
}
