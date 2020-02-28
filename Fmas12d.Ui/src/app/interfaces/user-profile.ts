import { BankDetailsProfile } from './bank-details-profile';
import { ContactDetailProfile } from './contact-detail-profile';
import { User } from './user';
import { UserSpeciality } from './user-speciality';

export interface UserProfile extends User {
  contactDetails: ContactDetailProfile[];
  contactDetailTypeId: number;
  emailAddress: string;
  financeDetails: BankDetailsProfile[];
	genderTypeId: number;
  gmcNumber?: number;
  isAmhp: boolean;
  isDoctor: boolean;
  isFinance: boolean;
	mobileNumber?: string;
  organisationName: string;
  profileTypeId: number;
	section12ApprovalStatusId?: number;
	section12ExpiryDate?: Date;
	telephoneNumber: string;	
	userSpecialities?: UserSpeciality[];	
}