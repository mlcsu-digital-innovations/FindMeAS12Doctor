import { Assessment } from './assessment.model';

export class UserAssessmentClaim {

  assessment: Assessment;
  assessmentPayment?: number;
  claimReference?: number;
  claimStatus: string;
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
