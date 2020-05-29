export class UserAssessmentClaimRequest {
  assessmentId: number;
  withinContract: boolean;
  startPostcode: string;
  endPostcode: string;
  previousAssessmentId?: number;
  nextAssessmentId?: number;
}
