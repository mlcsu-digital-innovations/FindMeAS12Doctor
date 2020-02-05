import { User } from './user';
import { BankDetails } from './bank-details';

export interface Claimant extends User {
  bankDetails: BankDetails;
  hasBankDetails: boolean;
}
