export interface ReferralEdit {
  createdAt: Date;
  id: number;
  isPlannedExamination: boolean;
  leadAmhpUserId: number;
  patientAlternativeIdentifier: string;
  patientCcgId: number;
  patientGpPracticeId: number;
  patientNhsNumber: number;
  patientResidentialPostcode: string;
  referralStatusName: string;
}
