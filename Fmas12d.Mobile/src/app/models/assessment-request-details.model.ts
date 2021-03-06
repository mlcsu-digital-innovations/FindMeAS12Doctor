import { AssessmentSelectedDoctor } from './assessment-selected-doctor.model';
import { DetailType } from './detail-type.model';

export class AssessmentRequestDetails {
  amhpUserContact: string;
  amhpUserName: string;
  detailTypes: DetailType[];
  doctorDetails: AssessmentSelectedDoctor;
  id: number;
  isPlanned: boolean;
  postcode: string;
  dateTime: Date;
}
