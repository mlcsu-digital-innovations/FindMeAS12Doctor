import { Patient } from './patient';
import { LeadAmhpUser } from './user';

export interface Referral {
  createdAt: Date;
  createdByUserId: number;
  examinations: any[];
  id: number;
  isPlannedExamination: boolean;
  leadAmhpUser: LeadAmhpUser;
  leadAmhpUserId: number;
  patient: Patient;
  patientId: number;
  referralStatusId: number;
}
