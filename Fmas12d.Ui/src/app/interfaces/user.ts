import { BankDetails } from './bank-details';

export interface User {
  bankDetails?: BankDetails[];
  displayName: string;
  id: number;
  isAmhp: boolean;
  isDoctor: boolean;
  isFinance: boolean;
  profileTypeId: number;
}
