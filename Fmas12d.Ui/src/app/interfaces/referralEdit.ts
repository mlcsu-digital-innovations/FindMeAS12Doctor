export interface ReferralEdit {
  createdAt: Date;
  id: number;
  isPlannedAssessment: boolean;
  leadAmhpUserId: number;
  leadAmhpUserDisplayName: string;
  patientAlternativeIdentifier: string;
  patientCcgId: number;
  patientCcgName: number;
  patientGpPracticeId: number;
  patientGpNameAndPostcode: number;
  patientNhsNumber: number;
  patientResidentialPostcode: string;
  statusName: string;
}
