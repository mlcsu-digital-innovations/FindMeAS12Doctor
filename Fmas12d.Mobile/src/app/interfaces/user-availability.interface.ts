import { Location } from './location.interface';

export interface UserAvailability {
  end: Date | string;
  id: number;
  location: Location;
  start: Date | string;
  statusId: number;
}

