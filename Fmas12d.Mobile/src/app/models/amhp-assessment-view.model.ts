import { AmhpExaminationViewDoctor } from './amhp-assessment-view-doctor.model';

export class AmhpExaminationView {
  address1: string;
  address2: string;
  address3: string;
  address4: string;
  dateTime: Date;
  doctorsAllocated: AmhpExaminationViewDoctor[];
  id: number;
  isSuccessful?: boolean;
  meetingArrangementComment: string;
  patientIdentifier: string;
  postcode: string;
  referralId: string;
  specialityName: string;

  constructor() { }
}