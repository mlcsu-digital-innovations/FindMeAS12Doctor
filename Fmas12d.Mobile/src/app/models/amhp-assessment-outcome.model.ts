import { AmhpExaminationViewDoctor } from './amhp-assessment-view-doctor.model';

export class AmhpExaminationOutcome {
  attendingDoctors: AmhpExaminationViewDoctor[];
  completedTime?: Date;
  examinationId: number;
  unsuccessfulExaminationTypeId?: number;

  constructor(
    completedTime?: Date,
    attendingDoctors?: AmhpExaminationViewDoctor[],
    examinationId?: number,
    unsuccessfulExaminationTypeId?: number) {
    if (attendingDoctors) {
      this.attendingDoctors = attendingDoctors;
    }

    if (examinationId) {
      this.examinationId = examinationId;
    }
    
    this.completedTime = completedTime;
    this.unsuccessfulExaminationTypeId = unsuccessfulExaminationTypeId;
  }
}