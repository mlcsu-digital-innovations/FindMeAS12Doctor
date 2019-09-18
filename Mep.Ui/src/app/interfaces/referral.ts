export interface Referral {
  CreatedAt: Date;
  CreatedByUserId: number;
  PatientId: number;
  ReferralStatusId: number;
  LeadAmhpUserId: number;
  IsPlannedExamination: boolean;
}
