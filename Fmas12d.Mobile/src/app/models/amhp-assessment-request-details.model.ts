import { AmhpAssessmentSelectedDoctor } from './amhp-assessment-selected-doctor.model';
import { DetailType } from './detail-type.model';

export class AmhpAssessmentRequestDetails {
  amhpUserName: string;
  detailTypes: DetailType[];
  doctorDetails: AmhpAssessmentSelectedDoctor;
  id: number;
  isPlanned: boolean;
  postcode: string;
  dateTime: Date;
}
