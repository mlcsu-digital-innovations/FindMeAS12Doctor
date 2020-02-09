import { ContactDetail } from './contact-detail';

export interface ContactDetailProfile extends ContactDetail {
	address: string;
	postcode: string;
	mobile: string;
}