import { AmhpAssessmentViewDoctor } from './amhp-assessment-view-doctor.model';

export class AmhpAssessmentOutcome {
  attendingDoctors: AmhpAssessmentViewDoctor[];
  completedTime?: Date;
  assessmentId: number;
  unsuccessfulAssessmentTypeId?: number;

  constructor(
    completedTime?: Date,
    attendingDoctors?: AmhpAssessmentViewDoctor[],
    assessmentId?: number,
    unsuccessfulAssessmentTypeId?: number) {
    if (attendingDoctors) {
      this.attendingDoctors = attendingDoctors;
    }

    if (assessmentId) {
      this.assessmentId = assessmentId;
    }
    
    this.completedTime = completedTime;
    this.unsuccessfulAssessmentTypeId = unsuccessfulAssessmentTypeId;
  }
}