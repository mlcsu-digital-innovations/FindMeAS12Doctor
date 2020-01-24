import { UserAssessmentClaim } from './user-assessment-claim.model';

export class AssessmentClaim {

  address1: string;
  assessmentDate: Date;
  claim: UserAssessmentClaim;
  completedTime: Date;
  id: number;
  isCompleted: boolean;
  isSuccessful?: boolean;
  postcode: string;
  referralStatus: string;
  unsuccessfulAssessmentType: string;

  constructor() { }
}
