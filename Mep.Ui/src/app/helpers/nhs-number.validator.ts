import { FormControl } from '@angular/forms';

// custom validator to check that two fields match
export function NhsNumberValidFormat(control: FormControl) {
  return ValidateNHSNumber(control.value)
    ? null
    : {
        NhsNumberValidFormat: { valid: false }
      };
}

function ValidateNHSNumber(nhsNumber: string): boolean {

  // a blank NHS number shouldn't be flagged as invalid
  if (nhsNumber === '') {
    return true;
  }

  return IsCheckDigitValid(nhsNumber);
}

function IsCheckDigitValid(nhsNumber: string): boolean {
  // NHS numbers are 10 digits long
  if (nhsNumber.length === 10) {
    const checkDigit = Number(nhsNumber[9]);

    let calculatedValue = 0;

    for (let x = 0; x < 9; x++) {
      calculatedValue = calculatedValue + (10 - x) * Number(nhsNumber[x]);
    }

    let calculatedDigit = 11 - (calculatedValue % 11);
    calculatedDigit = calculatedDigit === 11 ? 0 : calculatedDigit;

    return calculatedDigit === checkDigit;
  } else {
    return false;
  }
}
