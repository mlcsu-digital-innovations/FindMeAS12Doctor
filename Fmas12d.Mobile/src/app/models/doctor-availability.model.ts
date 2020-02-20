import { AVAILABLE, UNAVAILABLE, KNOWN_LOCATION_OTHER_ID } from '../constants/app.constants';
import { NameId } from '../interfaces/name-id.interface';
import { Location } from '../interfaces/location.interface';

export class DoctorAvailability {
  
  public dateErrorText: string = "Invalid start / end dates";
  public endDateTime: Date;
  public id: number;
  public isAvailable: boolean;
  public isPostcodeValid: boolean = false;  
  public startDateTime: Date;

  private _knownLocation: NameId = {id: 0, name: ""};
  private _postcode: string;

  constructor() {
  }

  get knownLocation(): NameId {
    return this._knownLocation;
  }

  set knownLocation(newKnownLocation) {
    this._knownLocation = newKnownLocation;
  }  

  get postcode(): string {
    return this._postcode;
  }

  set postcode(newPostcode: string) {
    this.isPostcodeValid = false;
    this._postcode = newPostcode;
  }

  get location(): Location {
    
    if (this.isAvailable) {
      return {
        contactDetailId: this.isPostcode()
        ? undefined
        : this.knownLocation.id,
        postcode: this.isPostcode()
        ? this.postcode
        : undefined,
      } as Location
    } else {
      return {
        contactDetailId: undefined,
        postcode: undefined
      } as Location
    }
  }  

  get statusId(): number {
    return this.isAvailable ? AVAILABLE : UNAVAILABLE;
  }

  compareWith(da: DoctorAvailability): boolean {
    return this.endDateTime === da.endDateTime &&
           this.isAvailable === da.isAvailable &&
           this.knownLocation.id === da.knownLocation.id &&
           this.postcode === da.postcode &&
           this.startDateTime === da.startDateTime;
  }

  compareKnownLocation(o1: NameId, o2: NameId): boolean {
    return o1 && o2 ? o1.id === o2.id : o1 === o2;
  }

  hasDateError(): boolean {
    return this.startDateTime >= this.endDateTime;
  }

  isPostcode(): boolean {
    return this.knownLocation.id === KNOWN_LOCATION_OTHER_ID;
  }

  isValid(): boolean {
    let isValidDate = !this.hasDateError();
    let isValidPostcode = this.isPostcode() ? this.isPostcodeValid : true;
    let isValidLocation = this.knownLocation.id !== null;
    return isValidDate && isValidPostcode && isValidLocation;
  }

}