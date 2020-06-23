import { Location } from './location';
import { UserAvailabilityStatus } from './user-availability-status';

export interface OnCallDoctorList {  
  end: Date;
  gmcNumber: number;
  id: number;
  location: Location;
  onCallConfirmationSentAt: Date;
  onCallIsConfirmed?: boolean;  
  onCallRejectedReason: string;
  start: Date;
  status: UserAvailabilityStatus;
  userId: number;
  userName: string;
}

