export interface CurrentExamination {
  amhpUserName: string;
  doctorNamesAccepted: string[];
  doctorNamesAllocated: string[];
  fullAddress: string;
  id: number;
  isPlanned: boolean;
  meetingArrangementComment: string;
  mustBeCompletedBy?: Date;
  postcode: string;
  preferredDoctorGenderTypeName: string;
  scheduledTime?: Date;
  specialityName: string;
}
