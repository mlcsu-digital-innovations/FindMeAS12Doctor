import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { SHORT_DATE_TIME } from 'src/app/constants/Constants';
import * as moment from 'moment';

@Pipe({ name: 'dateTimeFormat' })
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {
  transform(value: any, args?: any): string {
    return moment(value).format(SHORT_DATE_TIME);
  }
}
