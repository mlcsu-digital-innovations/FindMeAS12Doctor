import { KnownLocation } from './known-location.model';

export class AmhpAssessmentSelectedDoctor {
  id: number;
  displayName: string;
  distance: number;
  doctorId: number;
  gmcNumber: number;
  knownLocation: KnownLocation;
}
