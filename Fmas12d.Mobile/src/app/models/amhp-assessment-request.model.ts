import { AmhpAssessmentList } from './amhp-assessment-list.model';

export class AmhpAssessmentRequest extends AmhpAssessmentList {
  doctorStatusId?: number;
  doctorHasAccepted?: boolean;
  referralStatusId?: number;
}
