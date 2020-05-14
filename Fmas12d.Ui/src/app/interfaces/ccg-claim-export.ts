// do not order this class alphabetically !
export interface CcgClaimExport {
  claimReference: number;
  assessmentDate: Date;
  assessmentPostcode: string;
  successfulAssessment: boolean;
  id?: number;
  mileage: number;
  mileagePayment: number;
  assessmentPayment: number;
  totalPayment: number;
}
