import { Assessment } from './assessment';
import { Ccg } from './ccg';
import { Claimant } from './claimant';
import { NameIdList } from './name-id-list';

export interface ClaimView {
  assessment: Assessment;
  assessmentPayment: number;
  claimReference: string;
  claimStatus: NameIdList;
  ccg: Ccg;
  claimant: Claimant;
  id: number;
  mileagePayment: number;
}
