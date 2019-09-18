export interface PatientSearchResult  {
  alternativeIdentifier: string | null;
  ccgId?: number;
  gpPracticeId?: number;
  nhsNumber?: number;
  residentialPostcode?: string;
  currentReferralId?: number;
  patientId: number;
  gpPracticeNameAndPostcode?: string;
  ccgName?: string;
}
