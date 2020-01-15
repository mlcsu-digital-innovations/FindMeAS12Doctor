import { AvailableDoctor } from './available-doctor';

export interface AssessmentAvailability {

  address1: string;
  address2?: string;
  address3?: string;
  address4?: string;
  amhpName?: string;
  availableDoctors: AvailableDoctor[];
  dateTime: Date;
  id: number;
  isSuccessful: boolean;
  leadAmhpName: string;
  meetingArrangementComment?: string;
  patientIdentifier: string;
  postcode: string;
  preferredDoctorGenderTypeName?: string;
  referralId: number;
  specialityName?: string;
}
