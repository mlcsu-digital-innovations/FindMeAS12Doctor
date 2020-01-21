import { UserAssessmentClaim } from './user-assessment-claim-model';

export class AssessmentClaim {

  address1: string;
  assessmentDate: Date;
  claim: UserAssessmentClaim;
  completedTime: Date;
  id: number;
  isSuccessful?: boolean;
  postcode: string;
  unsuccessfulAssessmentTypeId?: boolean;

  constructor() { }
}
