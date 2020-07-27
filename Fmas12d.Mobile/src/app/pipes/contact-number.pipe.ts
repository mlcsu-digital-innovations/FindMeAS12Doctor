import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'contactNumber'
})
export class ContactNumberPipe implements PipeTransform {
  transform(val: string): string {

    if (val === undefined) {
      return '';
    }

    val = val.replace(/ /g, '');

    if (val.length === 11) {
      val = val.substring(0, 5) + ' ' + val.substring(5, 8) + ' ' + val.substring(8);
    }

    return val;
  }
}
