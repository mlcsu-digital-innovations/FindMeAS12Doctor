import { KnownLocation } from './known-location.model';
import { ContactDetail } from './contact-detail.model';

export class AssessmentSelectedDoctor {
  contactDetails: ContactDetail[];
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
