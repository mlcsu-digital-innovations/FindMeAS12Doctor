import { ContactDetail } from './contact-detail';

export interface UserDetails {
  contactDetailBase: ContactDetail;
  displayName: string;
  genderName: string;
  gmcNumber: number;
  id: number;
  profileTypeName: string;
  section12ApprovalStatusId: number;
  telephone: string;
  type: string;
  userSpecialityNames: string[];
}
