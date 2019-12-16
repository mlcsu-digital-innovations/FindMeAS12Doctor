import { AmhpAssessmentOutcomeDoctor } from './amhp-assessment-outcome-doctor.model';
import { AmhpAssessmentViewDoctor } from './amhp-assessment-view-doctor.model';

export class AmhpAssessmentOutcome {
  attendingDoctors: AmhpAssessmentOutcomeDoctor[];
  completedTime?: Date;
  assessmentId: number;
  unsuccessfulAssessmentTypeId?: number;

  constructor(
    completedTime?: Date,
    attendingDoctors?: AmhpAssessmentViewDoctor[],
    assessmentId?: number,
    unsuccessfulAssessmentTypeId?: number) {
    if (attendingDoctors) {           
      this.attendingDoctors = attendingDoctors.map((doctor: AmhpAssessmentViewDoctor) => 
        new AmhpAssessmentOutcomeDoctor(true, doctor.doctorId));
    }

    if (assessmentId) {
      this.assessmentId = assessmentId;
    }
    
    this.completedTime = completedTime;
    this.unsuccessfulAssessmentTypeId = unsuccessfulAssessmentTypeId;
  }
}