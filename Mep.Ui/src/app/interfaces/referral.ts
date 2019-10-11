import { Patient } from './patient';
import { LeadAmhpUser } from './user';
import { NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';

export interface Referral {
  createdAt: Date;
  createdByUserId: number;
  defaultToBeCompletedByDate: NgbDateStruct;
  defaultToBeCompletedByTime: NgbTimeStruct;
  examinations: any[];
  id: number;
  isPlannedExamination: boolean;
  leadAmhpUser: LeadAmhpUser;
  leadAmhpUserId: number;
  patient: Patient;
  patientId: number;
  referralCreatedAtAsDatePicker: NgbDateStruct;
  referralCreatedAtAsTimePicker: NgbTimeStruct;
  referralStatusId: number;
}
