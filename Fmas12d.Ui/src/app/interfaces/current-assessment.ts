import { AssessmentUser } from './assessment-user';
import { NameIdDescription } from './name-id-description';
import { User } from './user';

export interface CurrentAssessment {
  amhpUser: User;
  completedAt?: Date;
  detailTypes: NameIdDescription[];
  doctorsAllocated: AssessmentUser[];
  doctorsAttended: AssessmentUser[];
  doctorsSelected: AssessmentUser[];
  fullAddress: string;
  hasOutcome: boolean;
  id: number;
  isPlanned: boolean;
  isSuccessful: boolean;
  meetingArrangementComment: string;
  mustBeCompletedBy?: Date;
  postcode: string;
  preferredDoctorGenderType: NameIdDescription;
  scheduledTime?: Date;
  speciality: NameIdDescription;
  unsuccessfulAssessmentTypeIdName: string;
}
