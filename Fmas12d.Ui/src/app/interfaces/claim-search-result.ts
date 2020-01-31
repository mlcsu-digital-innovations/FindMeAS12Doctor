import { FinanceClaim } from './finance-claim';

export interface ClaimSearchResult {
  claims: FinanceClaim[];
  total: number;
}
