import { Assessment } from './assessment.model';
import { UserAssessmentClaim } from './user-assessment-claim.model';

export class UserAssessmentClaimList {
  assessments: Assessment[];
  claims: UserAssessmentClaim[];
}