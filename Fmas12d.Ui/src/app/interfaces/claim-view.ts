import { NameIdList } from './name-id-list';
import { Ccg } from './ccg';
import { User } from './user';

export interface ClaimView {
  claimReference: number;
  claimStatus: NameIdList;
  ccg: Ccg;
  claimant: User;
}
