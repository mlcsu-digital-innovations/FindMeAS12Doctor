import { BankDetailsProfile } from './bank-details-profile';
import { ContactDetailProfile } from './contact-detail-profile';
import { NameIdList } from './name-id-list';
import { User } from './user';

export interface UserProfile extends User {
  bankDetails: BankDetailsProfile[];
  contactDetails: ContactDetailProfile[];
  contactDetailTypeId: number;
  emailAddress: string;
  genderTypeId: number;
  gmcNumber?: number;
  isAdmin: boolean;
  isAmhp: boolean;
  isDoctor: boolean;
  isFinance: boolean;
  mobileNumber: string;
  organisationName: string;
  profileTypeId: number;
  section12ApprovalStatusId?: number;
  section12ExpiryDate?: Date;
  telephoneNumber: string;
  userSpecialities: NameIdList[];
}
