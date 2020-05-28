import { Assessment } from './assessment';
import { NameIdList } from './name-id-list';

export interface UserAssessmentClaim {
  assessment: Assessment;
  assessmentPayment: number;
  claimReference: string;
  claimStatus: NameIdList;
  exportedDate: Date;
  id: number;
  lastUpdated: Date;
  mileage: number;
  mileagePayment: number;
}
