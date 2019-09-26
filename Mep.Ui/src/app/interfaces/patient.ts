export interface Patient {
  alternativeIdentifier?: string;
  ccgId?: number;
  gpPracticeId?: number;
  id: number;
  isExistingPatient: boolean;
  nhsNumber?: number;
  residentialPostcode?: string;
}
