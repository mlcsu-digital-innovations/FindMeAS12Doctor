export class UserAssessmentClaim {

  assessmentPayment?: number;
  claimReference?: number;
  claimStatus: string;
  claimStatusId?: number;
  id: number;
  isClaimable: boolean;
  mileage: number;
  mileagePayment: number;
  paymentDate?: Date;
  travelComments?: string;

  constructor() { }
}
