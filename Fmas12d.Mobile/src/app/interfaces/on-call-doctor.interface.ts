import { UserAvailability } from './user-availability.interface';

export interface OnCallDoctor extends UserAvailability {
  onCallConfirmationSentAt: Date;
  onCallIsConfirmed?: boolean;
  onCallRejectedReason: string;  
}