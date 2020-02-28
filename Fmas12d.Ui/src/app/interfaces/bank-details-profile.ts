import { BankDetails } from './bank-details';

export interface BankDetailsProfile extends BankDetails {
  id: number;
  ccgId: number;
  ccgName: string;  
}