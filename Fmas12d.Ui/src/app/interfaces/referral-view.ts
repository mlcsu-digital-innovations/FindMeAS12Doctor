import { CurrentExamination } from './current-assessment';
import { PreviousExamination } from './previous-assessment';

export interface ReferralView {
  currentExamination: CurrentExamination;
  id: number;
  leadAmhp: string;
  patientIdentifier: string;
  previousExaminations: PreviousExamination;
  status: string;
}
