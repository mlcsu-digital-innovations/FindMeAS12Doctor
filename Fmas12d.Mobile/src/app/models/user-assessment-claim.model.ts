import { Assessment } from './assessment.model';
import { ClaimStatus } from './claim-status.model';

export class UserAssessmentClaim {

  assessment: Assessment;
  assessmentPayment?: number;
  claimReference?: number;
  claimStatus: ClaimStatus;
  claimStatusId?: number;
  hasClaim: boolean;
  id: number;
  isClaimable: boolean;
  lastUpdated: Date;
  mileage: number;
  mileagePayment: number;
  paymentDate?: Date;
  travelComments?: string;

  constructor() { }
}
