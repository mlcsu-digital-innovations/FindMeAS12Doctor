export interface ReferralList {
  assessmentLocationPostcode: string;
  currentAssessmentId: number;
  doctorsAllocated: number;
  doctorsSelected: number;
  doctorsSelectedAllocated: string;
  leadAmhp: string;
  numberOfAssessmentAttempts: number;
  patientIdentifier: string;
  patientId: number;
  referralId: number;
  referralStatusId: number;
  responsesAccepted: number;
  responsesReceived: number;
  responsesReceivedAccepted: string;
  specialityName: string;
  statusName: string;
  timescale: Date;
}
