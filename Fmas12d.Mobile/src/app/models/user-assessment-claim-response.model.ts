export class UserAssessmentClaimResponse {
  assessmentId: number;
  assessmentPayment: number;
  mileage: number;
  mileagePayment: number;
  ownPatient: boolean;
  startContactDetailType?: number;
  endContactDetailType?: number;
}
