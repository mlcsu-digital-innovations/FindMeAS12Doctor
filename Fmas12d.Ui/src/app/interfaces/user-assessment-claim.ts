import { Assessment } from './assessment';
import { NameIdList } from './name-id-list';

export interface UserAssessmentClaim {
  assessment: Assessment;
  claimReference: number;
  claimStatus: NameIdList;
  mileage: number;
  mileagePayment: number;
  assessmentPayment: number;
}
