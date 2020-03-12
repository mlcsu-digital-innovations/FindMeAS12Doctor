import { AssessmentOutcomeDoctor } from './assessment-outcome-doctor.model';
import { AssessmentViewDoctor } from './assessment-view-doctor.model';

export class AmhpAssessmentOutcome {
  attendingDoctors: AssessmentOutcomeDoctor[];
  completedTime?: Date;
  assessmentId: number;
  unsuccessfulAssessmentTypeId?: number;

  constructor(
    completedTime?: Date,
    attendingDoctors?: AssessmentViewDoctor[],
    assessmentId?: number,
    unsuccessfulAssessmentTypeId?: number) {
    if (attendingDoctors) {
      this.attendingDoctors = attendingDoctors.map((doctor: AssessmentViewDoctor) =>
        new AssessmentOutcomeDoctor(true, doctor.doctorId));
    }

    if (assessmentId) {
      this.assessmentId = assessmentId;
    }
    
    this.completedTime = completedTime;
    this.unsuccessfulAssessmentTypeId = unsuccessfulAssessmentTypeId;
  }
}
