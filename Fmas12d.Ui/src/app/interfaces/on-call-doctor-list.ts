import { Location } from './location';

export interface OnCallDoctorList {  
  end: Date;
  gmcNumber: number;
  id: number;
  isOnCall: boolean;
  location: Location;
  onCallConfirmationSentAt: Date;
  onCallIsConfirmed?: boolean;  
  onCallRejectedReason: string;
  start: Date;
  statusName: string;
  userId: number;
  userName: string;
}

