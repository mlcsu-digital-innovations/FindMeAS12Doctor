import { ActiveAssessment } from './active-assessment';

export interface AvailableDoctor {
  activeAssessments: ActiveAssessment[];
  distance: number;
  genderName: string;
  name: string;
  selected: boolean;
  specialityNames: string[];
  type: string;
}
