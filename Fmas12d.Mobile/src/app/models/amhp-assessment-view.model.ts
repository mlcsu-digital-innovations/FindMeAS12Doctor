import { AmhpAssessmentViewDoctor } from './amhp-assessment-view-doctor.model';
import { AmhpAssessmentSelectedDoctor } from './amhp-assessment-selected-doctor.model';
import { DetailType } from './detail-type.model';

export class AmhpAssessmentView {
  address1: string;
  address2: string;
  address3: string;
  address4: string;
  dateTime: Date;
  detailTypes: DetailType[];
  doctorsAllocated: AmhpAssessmentViewDoctor[];
  doctorsSelected: AmhpAssessmentSelectedDoctor[];
  id: number;
  isSuccessful?: boolean;
  meetingArrangementComment: string;
  patientIdentifier: string;
  postcode: string;
  referralId: string;
  specialityName: string;

  constructor() { }
}