import { AssessmentUser } from './assessment-user';

export interface CurrentAssessment {
  amhpUserName: string;
  doctorsAllocated: AssessmentUser[];
  doctorsSelected: AssessmentUser[];
  fullAddress: string;
  id: number;
  isPlanned: boolean;
  meetingArrangementComment: string;
  mustBeCompletedBy?: Date;
  postcode: string;
  preferredDoctorGenderTypeName: string;
  scheduledTime?: Date;
  specialityName: string;
}
