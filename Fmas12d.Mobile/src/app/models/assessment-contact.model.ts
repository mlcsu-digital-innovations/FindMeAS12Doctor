import { ContactDetailType } from './contact-detail-type.model';

export class AssessmentContact {
  address1: string;
  address2: string;
  address3: string;
  address4: string;
  amhpUserName: string;
  completedTime: Date;
  isSuccessful: boolean;
  postcode: string;
  scheduledTime: Date;
  unsuccessfulAssessmentTypeName: string;
  userContactDetailTypes: ContactDetailType[];

  constructor() {}
}