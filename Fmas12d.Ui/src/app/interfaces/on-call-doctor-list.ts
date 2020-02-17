import { Location } from './location';

export interface OnCallDoctorList {  
  onCallConfirmationSentAt: Date;
  onCallIsConfirmed?: boolean;  
  onCallRejectedReason: string;
  userName: string;
  end: Date;
  gmcNumber: number;
  id: number;
  location: Location;
  start: Date;
  userId: number;
}

