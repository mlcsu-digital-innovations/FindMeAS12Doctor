export interface DoctorClaimExport {
  claimReference: number;
  claimStatus: string;
  assessmentDate: string;
  assessmentPostcode: string;
  successfulAssessment: boolean;
  mileage: number;
  mileagePayment: number;
  assessmentPayment: number;
  totalPayment: number;
}
