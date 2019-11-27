import { AssessmentUser } from './assessment-user';
import { NameIdDescription } from './name-id-description';
import { User } from './user';

export interface CurrentAssessment {
  amhpUser: User;
  detailTypes: NameIdDescription[];
  doctorsAllocated: AssessmentUser[];
  doctorsSelected: AssessmentUser[];
  fullAddress: string;
  id: number;
  isPlanned: boolean;
  meetingArrangementComment: string;
  mustBeCompletedBy?: Date;
  postcode: string;
  preferredDoctorGenderType: NameIdDescription;
  scheduledTime?: Date;
  speciality: NameIdDescription;
}
