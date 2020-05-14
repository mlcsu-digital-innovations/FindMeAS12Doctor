import { BankDetails } from './bank-details';

export interface User {
  displayName: string;
  id: number;
  isAmhp: boolean;
  isDoctor: boolean;
  isFinance: boolean;
  bankDetails?: BankDetails[];
}
