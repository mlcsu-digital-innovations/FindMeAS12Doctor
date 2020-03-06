import { Ccg } from './ccg';

export interface BankDetailsProfile {
  id: number;
  ccg: Ccg;  
  ccgId: number;  
  vsrNumber: number;
}