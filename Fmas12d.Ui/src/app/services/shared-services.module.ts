import { NgModule, ModuleWithProviders } from '@angular/core';
import { UserDetailsService } from './user/user-details.service';

@NgModule({

})
export class SharedServicesModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedServicesModule,
      providers: [
        UserDetailsService
      ]
    };
  }
}
