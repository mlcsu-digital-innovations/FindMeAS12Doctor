import { ActiveAssessment } from './active-assessment';

export interface AvailableDoctor {
  activeAssessments: ActiveAssessment[];
  distance: number;
  genderName: string;
  hasAccepted: boolean;
  hasResponded: boolean;
  id: number;
  isSelected: boolean;
  name: string;
  specialityNames: string[];
  type: string;
}
