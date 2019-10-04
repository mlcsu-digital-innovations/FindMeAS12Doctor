import { Injectable, TemplateRef } from '@angular/core';
import { ToastOptions } from 'src/app/interfaces/toast-options';

@Injectable({ providedIn: 'root' })
export class ToastService {
  toasts: any[] = [];

  show(textOrTpl: string | TemplateRef<any>, options: any = {}) {

    options.classname += ' text-light fade-in toast-container';

    this.toasts.push({ textOrTpl, ...options });
  }

  displayError(textOrTpl: string | TemplateRef<any>, options: ToastOptions) {
    options.delay = 60000;
    options.classname = 'bg-danger';
    options.hide = false;
    options.iconClass = 'fas fa-times-circle';
    this.show(textOrTpl, options);
  }

  displayWarning(textOrTpl: string | TemplateRef<any>, options: ToastOptions) {
    options.classname = 'bg-warning';
    options.iconClass = 'fas fa-exclamation-triangle';
    this.show(textOrTpl, options);
  }

  displayInfo(textOrTpl: string | TemplateRef<any>, options: ToastOptions) {
    options.classname = 'bg-info';
    options.iconClass = 'fas fa-info-circle';
    this.show(textOrTpl, options);
  }

  displaySuccess(textOrTpl: string | TemplateRef<any>, options: ToastOptions) {
    options.classname = 'bg-success';
    options.iconClass = 'fas fa-check';
    this.show(textOrTpl, options);
  }

  remove(toast) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }
}
