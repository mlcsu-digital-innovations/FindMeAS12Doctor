export interface Examination {
  address1?: string;
  address2?: string;
  address3?: string;
  address4?: string;
  amhpId: number;
  isPlanned: boolean;
  meetingArrangementComment?: string;
  mustBeCompletedBy?: Date;
  postcode: string;
  preferredDoctorGenderTypeId?: number;
  referralId: number;
  scheduledTime?: Date;
  specialityId?: number;
}
