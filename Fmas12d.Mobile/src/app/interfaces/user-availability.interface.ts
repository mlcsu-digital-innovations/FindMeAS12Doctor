export interface UserAvailability {
  contactDetailId?: number;
  end: Date | string;
  id: number;
  isAvailable: boolean;
  location: Location;
  postcode?: string;
  start: Date | string;
  statusId: number;
}

export interface Location {
  contactDetailId?: number;
  contactDetailTypeName?: string;
  latitude?: number;
  longitude?: number;
  postcode?: string;
  type: string;
}
