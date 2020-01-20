export interface OnCallDoctorList {
  confirmationReasonAndDetails: string;
  confirmationRequestSentDateTime: Date;
  confirmationStatus: boolean;
  doctorName: string;
  endDateTime: Date;
  gmcNumber: number;
  id: number;
  location: string;
  startDateTime: Date;
}