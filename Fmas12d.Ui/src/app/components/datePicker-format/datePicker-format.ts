import { Injectable } from '@angular/core';
import { NgbDateParserFormatter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Injectable()
export class NgbDateCustomParserFormatter extends NgbDateParserFormatter {
  parse(value: string): NgbDateStruct {
    if (value) {
      const dateParts = value.trim().split('/');
      if (dateParts.length === 1 &&
          this.isNumber(dateParts[0])) {
            return {
              day: this.toInteger(dateParts[0]),
              month: null,
              year: null
            };
      } else if ( dateParts.length === 2 &&
                  this.isNumber(dateParts[0]) &&
                  this.isNumber(dateParts[1])) {
                    return {
                      day: this.toInteger(dateParts[0]),
                      month: this.toInteger(dateParts[1]),
                      year: null
                    };
      } else if ( dateParts.length === 3 &&
                  this.isNumber(dateParts[0]) &&
                  this.isNumber(dateParts[1]) &&
                  this.isNumber(dateParts[2])) {
                    return {
                      day: this.toInteger(dateParts[0]),
                      month: this.toInteger(dateParts[1]),
                      year: this.toInteger(dateParts[2])
                    };
      }
    }
    return null;
  }

  isNumber(value: any): boolean {
    return (
      (value !== null) &&
      !isNaN(Number(value.toString()))
    );
  }

  toInteger(value: any): number {
    return +value;
  }

  padNumber(value: any): string {
    const fullString = `0${value}`;
    return fullString.substr(fullString.length - 2, 2);
  }

  format(date: NgbDateStruct): string {
    return date ?
      `${this.isNumber(date.day) ?
        this.padNumber(date.day) :
        ''}/${this.isNumber(date.month) ?
          this.padNumber(date.month) :
          ''}/${date.year}` :
      '';
  }
}
