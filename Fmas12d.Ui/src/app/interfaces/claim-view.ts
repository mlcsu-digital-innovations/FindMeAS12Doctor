import { NameIdList } from './name-id-list';
import { Ccg } from './ccg';
import { User } from './user';
import { Claimant } from './claimant';

export interface ClaimView {
  assessmentPayment: number;
  claimReference: number;
  claimStatus: NameIdList;
  ccg: Ccg;
  claimant: Claimant;
  id: number;
  mileagePayment: number;
}
