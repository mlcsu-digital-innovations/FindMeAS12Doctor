export interface AvailableDoctor {
  availabilityDetails?: string;
  distanceFromAssessment: number;
  doctorGender?: string;
  doctorName: string;
  doctorSpeciality?: string;
  doctorType?: string;
  id: number;
  otherAssessmentDetails?: string;
  selected?: boolean;
}
