import { Injectable } from '@angular/core';
import { ToastOptions } from 'src/app/models/toast-options.model';
import { ToastController } from '@ionic/angular'

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  constructor(private toastCtrl: ToastController) { }

  public displayError(options: ToastOptions) {
    options.showCloseButton = true;
    options.color = 'danger'
    this.show(options);
  }

  public displaySuccess(options: ToastOptions) {
    options.duration = 2000;
    options.color = 'success';
    this.show(options);
  }

  public displayMessage(options: ToastOptions) {
    options.duration = 3000;
    options.color = 'primary';
    this.show(options);
  }

  private async show(options: any = {}) {
    options.animated = true;
    options.position = 'top';
    const toast = await this.toastCtrl.create(options);
    toast.present();
  }
}
