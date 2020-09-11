import { AssessmentOutcomeDoctor } from './assessment-outcome-doctor';

export interface AssessmentOutcome {
  assessmentId: number;
  attendingDoctors: AssessmentOutcomeDoctor[];
  completedTime?: Date;
  unsuccessfulAssessmentTypeId?: number;
}
