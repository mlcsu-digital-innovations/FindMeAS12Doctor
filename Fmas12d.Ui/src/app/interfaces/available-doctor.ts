import { ActiveAssessment } from './active-assessment';
import { ContactDetail } from './contact-detail';

export interface AvailableDoctor {
  activeAssessments: ActiveAssessment[];
  contactDetails: ContactDetail[];
  distance: number;
  end: Date;
  genderName: string;
  hasAccepted: boolean;
  hasResponded: boolean;
  id: number;
  isSelected: boolean;
  name: string;
  specialityNames: string[];
  section12ApprovalStatusName: string;
  section12ExpiryDate: Date;
  start: Date;
  type: string;
}
