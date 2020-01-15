import { ContactDetail } from './contact-detail';

export interface UserDetails {
  contactDetailBase: ContactDetail;
  displayName: string;
  gender: string;
  genderName: string;
  genderTypeId: number;
  gmcNumber: number;
  id: number;
  profileTypeId: number;
  profileTypeName: string;
  section12ApprovalStatusId?: number;
  specialities: string[];
  telephone: string;
  type: string;
  userSpecialityNames: string[];
}
