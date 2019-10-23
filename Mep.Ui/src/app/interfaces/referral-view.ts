import { CurrentExamination } from './current-examination';
import { PreviousExamination } from './previous-examination';

export interface ReferralView {
  currentExamination: CurrentExamination;
  id: number;
  leadAmhp: string;
  patientIdentifier: string;
  previousExaminations: PreviousExamination;
  status: string;
}
