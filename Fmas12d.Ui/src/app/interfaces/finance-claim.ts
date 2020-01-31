import { Assessment } from './assessment';
import { NameIdList } from './name-id-list';
import { User } from './user';
import { Ccg } from './ccg';

export interface FinanceClaim {
  assessment: Assessment;
  ccg: Ccg;
  claimant: User;
  claimReference: number;
  claimStatus: NameIdList;
}
