export interface Referral {
  createdAt: Date;
  createdByUserId: number;
  isPlannedExamination: boolean;
  leadAmhpUserId: number;
  patientId: number;
  referralStatusId: number;
}
