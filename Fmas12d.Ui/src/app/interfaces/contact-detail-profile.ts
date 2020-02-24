import { ContactDetail } from './contact-detail';

export interface ContactDetailProfile extends ContactDetail {	
  address1: string;
  address2?: string;
  address3?: string;
  contactDetailTypeId: number
  mobileNumber: string;
  postcode: string;
  town?: string;  
}