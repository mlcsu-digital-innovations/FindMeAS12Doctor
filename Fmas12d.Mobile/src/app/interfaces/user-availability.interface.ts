export interface UserAvailability {
  end: Date;
  id: number;
  isAvailable: boolean;
  locationDetails?: string;
  start: Date;
  contactDetailId?: number;
  postcode?: string;
  latitude?: number;
  longitude?: number;
}
