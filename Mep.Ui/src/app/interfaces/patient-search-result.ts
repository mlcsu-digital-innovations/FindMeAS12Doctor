export interface PatientSearchResult  {
  alternativeIdentifier: string | null;
  ccgId: number | null;
  gpPracticeId: number | null;
  nhsNumber: number | null;
  residentialPostcode: string | null;
  currentReferralId: number | null;
  patientId: number;
  gpPracticeNameAndPostcode: string | null;
  ccgName: string | null;
}
