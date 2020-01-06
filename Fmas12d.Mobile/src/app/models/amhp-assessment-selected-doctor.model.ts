import { KnownLocation } from './known-location.model';

export class AmhpAssessmentSelectedDoctor {  
  displayName: string;
  distance: number;
  doctorId: number;
  gmcNumber: number;
  hasAccepted: boolean;
  hasResponded?: boolean;
  id: number;
  isRegistered?: boolean;
  isS12?: boolean;
  knownLocation: KnownLocation;
}
