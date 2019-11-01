export interface PatientSearchResult  {
  alternativeIdentifier: string | null;
  ccgId: number | null;
  ccgName: string | null;
  currentReferralId: number | null;
  gpPracticeId: number | null;
  gpPracticeNameAndPostcode: string | null;
  leadAmhp: string | null;
  nhsNumber: number | null;
  patientId: number;
  residentialPostcode: string | null;
}
