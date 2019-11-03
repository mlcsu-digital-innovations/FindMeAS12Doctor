import { AmhpAssessmentViewDoctor } from './amhp-assessment-view-doctor.model';

export class AmhpAssessmentView {
  address1: string;
  address2: string;
  address3: string;
  address4: string;
  dateTime: Date;
  doctorsAllocated: AmhpAssessmentViewDoctor[];
  id: number;
  isSuccessful?: boolean;
  meetingArrangementComment: string;
  patientIdentifier: string;
  postcode: string;
  referralId: string;
  specialityName: string;

  constructor() { }
}