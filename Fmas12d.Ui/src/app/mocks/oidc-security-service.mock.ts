import { of, Observable } from 'rxjs';

export class OidcSecurityServiceStub {

  public onAuthorizationResult = new Observable();
  public onModuleSetup = new Observable();

  authorize() {}

  getToken() {
    return 'some_token_xxxxxxxx';
  }

  getIsAuthorized() {
    return of({});
  }

  getUserData() {
    return of({});
  }

  moduleSetup() {
    return true;
  }

}
