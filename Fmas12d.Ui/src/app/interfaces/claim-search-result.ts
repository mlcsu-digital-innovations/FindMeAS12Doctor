import { UserAssessmentClaim } from './user-assessment-claim';

export interface ClaimSearchResult {
  claims: UserAssessmentClaim[];
  total: number;
}
