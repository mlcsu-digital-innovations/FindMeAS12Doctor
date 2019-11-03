import { Patient } from './patient';
import { LeadAmhpUser } from './user';

export interface Referral {
  createdAt: Date;
  createdByUserId: number;
  defaultToBeCompletedBy: Date;
  assessments: any[];
  id: number;
  isPlannedAssessment: boolean;
  leadAmhpUser: LeadAmhpUser;
  leadAmhpUserId: number;
  patient: Patient;
  patientId: number;
  referralStatusId: number;
}
