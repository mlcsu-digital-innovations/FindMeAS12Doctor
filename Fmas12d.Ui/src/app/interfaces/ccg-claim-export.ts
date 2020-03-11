export interface CcgClaimExport {
  claimReference: number;
  assessmentDate: Date;
  assessmentPostcode: string;
  successfulAssessment: boolean;
  mileage: number;
  mileagePayment: number;
  assessmentPayment: number;
  totalPayment: number;
}
