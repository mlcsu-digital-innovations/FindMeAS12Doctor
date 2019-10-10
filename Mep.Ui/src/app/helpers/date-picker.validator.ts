import { FormControl } from '@angular/forms';

// custom validator to check that the field uses a valid date format
export function DatePickerFormat(control: FormControl) {

  if (typeof control.value === 'string') {
    return ValidateDateFormat(control.value)
      ? null
      : {
        DatePickerFormat: { valid: false }
      };
  } else {
    return null;
  }
}

function ValidateDateFormat(checkDate: string): boolean {

  // pattern is 'dd/mm/yyyy'
  const pattern = new RegExp('^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$');

  return pattern.test(checkDate);
}
