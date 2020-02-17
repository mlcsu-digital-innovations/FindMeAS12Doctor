import { ContactDetail } from './contact-detail';

export interface ContactDetailType {
  id?: number;
  name: string;
  contactDetails: ContactDetail[]
}