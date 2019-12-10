import { AmhpAssessmentSelectedDoctor } from './amhp-assessment-selected-doctor.model';
import { DetailType } from './detail-type.model';

export class AmhpAssessmentRequestDetails {
  detailTypes: DetailType[];
  doctorDetails: AmhpAssessmentSelectedDoctor;
  id: number;
  postcode: string;
  dateTime: Date;
}
