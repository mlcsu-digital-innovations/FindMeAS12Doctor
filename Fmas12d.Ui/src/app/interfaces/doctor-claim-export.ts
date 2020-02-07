export interface DoctorClaimExport {
  claimReference: number;
  claimStatus: string;
  assessmentDate: Date;
  assessmentPostcode: string;
  successfulAssessment: boolean;
  mileage: number;
  mileagePayment: number;
  assessmentPayment: number;
  totalPayment: number;
}
