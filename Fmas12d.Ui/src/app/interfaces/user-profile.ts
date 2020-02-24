import { ContactDetailProfile } from './contact-detail-profile';
import { User } from './user';
import { UserSpeciality } from './user-speciality';

export interface UserProfile extends User {
  contactDetails: ContactDetailProfile[];
  contactDetailTypeId: number;
	emailAddress: string;
	genderTypeId: number;
	gmcNumber?: number;
	mobileNumber?: string;
  organisationName: string;
  profileTypeId: number;
	section12ApprovalStatusId?: number;
	section12ExpiryDate?: Date;
	telephoneNumber: string;	
	userSpecialities?: UserSpeciality[];
	vsrNumber?: string;  
}