import { UnsuccessfulAssessmentType } from './unsuccessful-assessment-type.model';
import { UserInfo } from '@ionic-native/ms-adal/ngx';
import { UserDetails } from '../interfaces/user-details';

export class Assessment {

  address1: string;
  address2: string;
  address3: string;
  address4: string;
  amhpUser: UserDetails;
  completedTime: Date;
  hasBeenCompleted: boolean;
  hasBeenReviewed: boolean;
  isSuccessful: boolean;
  postcode: string;
  unsuccessfulAssessmentType: UnsuccessfulAssessmentType;

  constructor() { }
}
