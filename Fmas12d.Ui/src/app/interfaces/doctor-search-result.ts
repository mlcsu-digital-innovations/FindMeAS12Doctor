import { NameIdList } from './name-id-list';

export interface DoctorSearchResult extends NameIdList {
  fromSection12LiveRegister: boolean;
}
