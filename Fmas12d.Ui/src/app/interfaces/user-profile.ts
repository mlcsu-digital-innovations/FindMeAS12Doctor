import { BankDetailsProfile } from './bank-details-profile';
import { ContactDetailProfile } from './contact-detail-profile';
import { NameIdList } from './name-id-list';
import { User } from './user';

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
	specialities?: NameIdList[];	
}