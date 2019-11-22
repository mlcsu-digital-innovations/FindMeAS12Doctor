import { Patient } from './patient';
import { User } from './user';

export interface Referral {
  createdAt: Date;
  createdByUserId: number;
  defaultToBeCompletedBy: Date;
  assessments: any[];
  id: number;
  isPlannedAssessment: boolean;
  leadAmhpUser: User;
  leadAmhpUserId: number;
  patient: Patient;
  patientId: number;
  referralStatusId: number;
}
