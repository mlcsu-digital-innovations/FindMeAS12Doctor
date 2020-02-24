import { AmhpAssessmentViewDoctor } from './amhp-assessment-view-doctor.model';
import { AmhpAssessmentSelectedDoctor } from './amhp-assessment-selected-doctor.model';
import { DetailType } from './detail-type.model';

export class AmhpAssessmentView {
  address1: string;
  address2: string;
  address3: string;
  address4: string;
  amhpUserName: string;
  canUpdateOutcome: boolean;
  dateTime: Date;
  detailTypes: DetailType[];
  doctorsAllocated: AmhpAssessmentViewDoctor[];
  doctorsSelected: AmhpAssessmentSelectedDoctor[];
  hasBeenReviewed: boolean;
  hasOutcome: boolean;
  id: number;
  isPlanned: boolean;
  isSuccessful?: boolean;
  meetingArrangementComment: string;
  patientIdentifier: string;
  postcode: string;
  referralId: string;
  referralStatus: string;
  referralStatusId: number;
  specialityName: string;

  constructor() { }
}