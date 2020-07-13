import { Injectable } from '@angular/core';
import { ToastOptions } from 'src/app/interfaces/toast-options';
import { UserDetailsService } from '../user/user-details.service';
import { User } from 'src/app/interfaces/user';

@Injectable({ providedIn: 'root' })
export class ToastService {
  toasts: any[] = [];

  constructor(private userDetailsService: UserDetailsService) {

  }

  showWithOptions(options: ToastOptions, classname: string, iconClass: string) {
    options.classname = classname;
    options.iconClass = iconClass;
    this.show(options);
  }

  show(options: any = {}) {
    options.classname += ' text-light fade-in toast-container';
    this.toasts.push({ ...options });
  }

  displayNotification(options: ToastOptions) {

    this.userDetailsService.getCurrentUserDetails().subscribe((user: User) => {
      console.log(options.audience, ' === ', user.profileTypeId);
      if (options.audience === user.profileTypeId) {
        this.displayInfo(options);
      }
    });
  }

  displayError(options: ToastOptions) {
    options.delay = 60000;
    options.autohide = false;
    this.showWithOptions(options, 'bg-danger', 'fas fa-times-circle');
  }

  displayWarning(options: ToastOptions) {
    this.showWithOptions(options, 'bg-warning', 'fas fa-exclamation-triangle');
  }

  displayInfo(options: ToastOptions) {
    this.showWithOptions(options, 'bg-info', 'fas fa-info-circle');
  }

  displaySuccess(options: ToastOptions) {
    this.showWithOptions(options, 'bg-success', 'fas fa-check');
  }

  remove(toast) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }
}
