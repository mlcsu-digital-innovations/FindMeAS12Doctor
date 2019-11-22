import { AssessmentAvailability } from './assessment-availability';
import { AvailableDoctor } from './available-doctor';

export interface AssessmentSelected extends AssessmentAvailability {
  doctorsSelected: AvailableDoctor[];
}
