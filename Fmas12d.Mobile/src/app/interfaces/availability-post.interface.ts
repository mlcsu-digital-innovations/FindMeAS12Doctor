import { Location } from './user-availability.interface';

export interface UserAvailabilityPost extends Location {
  end: Date | string;
  isAvailable: boolean;
  start: Date | string;
  statusId: number;
}
