export interface Patient {
  Id: number;
  AlternativeIdentifier?: string;
  NhsNumber?: number;
  CcgId?: number;
  GpPracticeId?: number;
  ResidentialPostcode?: string;
  IsExistingPatient: boolean;
}
