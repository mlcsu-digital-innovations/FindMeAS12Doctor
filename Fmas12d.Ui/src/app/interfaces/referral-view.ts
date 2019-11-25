import { CurrentAssessment } from './current-assessment';
import { PreviousAssessment } from './previous-assessment';

export interface ReferralView {
  createdAt: Date;
  currentAssessment: CurrentAssessment;
  id: number;
  leadAmhp: string;
  patientIdentifier: string;
  previousAssessments: PreviousAssessment;
  status: string;
}
