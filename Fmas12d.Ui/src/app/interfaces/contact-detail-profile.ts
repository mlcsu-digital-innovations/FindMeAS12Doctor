import { ContactDetail } from './contact-detail';

export interface ContactDetailProfile extends ContactDetail {	
  address1: string;
  address2: string;
  address3: string;
  contactDetailTypeId: number;
  contactDetailTypeName: string;
  emailAddress: string;
  latitude?: number;
  longitude?: number;
  mobileNumber: string;
  postcode: string;
  town: string;  
}