import { User } from './user';
import { Ccg } from './ccg';
import { UserAssessmentClaim } from './user-assessment-claim';

export interface FinanceClaim extends UserAssessmentClaim{
  ccg: Ccg;
  claimant: User;
}
