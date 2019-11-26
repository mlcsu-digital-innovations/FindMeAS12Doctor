import { AssessmentUser } from './assessment-user';
import { NameIdDescription } from './name-id-description';

export interface CurrentAssessment {
  amhpUserName: string;
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
