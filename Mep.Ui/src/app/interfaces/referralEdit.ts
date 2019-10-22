export interface ReferralEdit {
  createdAt: Date;
  id: number;
  isPlannedExamination: boolean;
  leadAmhpUserId: number;
  leadAmhpUserDisplayName: string;
  patientAlternativeIdentifier: string;
  patientCcgId: number;
  patientCcgName: number;
  patientGpPracticeId: number;
  patientGpNameAndPostcode: number;
  patientNhsNumber: number;
  patientResidentialPostcode: string;
  status: string;
}
