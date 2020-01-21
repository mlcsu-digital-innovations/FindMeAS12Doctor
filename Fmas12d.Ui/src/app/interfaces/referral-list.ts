export interface ReferralList {
  assessmentLocationPostcode: string;
  currentAssessmentId: number;
  doctorsAllocated: number;
  doctorsAttended: number;
  doctorsSelected: number;
  doctorsSelectedAllocatedAttended: string;
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
