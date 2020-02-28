export interface Assessment {
  address1?: string;
  address2?: string;
  address3?: string;
  address4?: string;
  amhpUserId: number;
  detailTypeIds: number[];
  id: number;
  isPlanned: boolean;
  isSuccessful: boolean;
  meetingArrangementComment?: string;
  mustBeCompletedBy?: Date;
  postcode: string;
  preferredDoctorGenderTypeId?: number;
  referralId: number;
  scheduledTime?: Date;
  specialityId?: number;
}
