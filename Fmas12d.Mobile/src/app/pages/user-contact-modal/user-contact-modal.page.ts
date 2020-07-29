import { AlertController, ModalController } from '@ionic/angular';
import { CallNumber } from '@ionic-native/call-number/ngx';
import { Component, Input, OnInit } from '@angular/core';
import { ContactDetail } from 'src/app/models/contact-detail.model';
import { ToastService } from 'src/app/services/toast/toast.service';
import { ContactNumberPipe } from 'src/app/pipes/contact-number.pipe';

@Component({
  selector: 'user-contact-modal',
  templateUrl: './user-contact-modal.page.html',
  styleUrls: ['./user-contact-modal.page.scss'],
  providers: [ContactNumberPipe]
})
export class UserContactModalPage implements OnInit {

  public contacts: {description, contactNumber}[] = [];

  constructor(
    private alertController: AlertController,
    private callNumber: CallNumber,
    private modalController: ModalController,
    private toastService: ToastService,
    private contactNumberPipe: ContactNumberPipe
  ) { }

  @Input() userName: string;
  @Input() contactDetails: ContactDetail[];

  callContact(contactNumber: string) {

    if (!isNaN(+contactNumber)) {
      this.callNumber.callNumber(contactNumber, true)
      .then(res => this.closeModal())
      .catch(err => this.toastService.displayError({message: 'Unable to launch dialler'}));
    }
  }

  transformNumber(contactNumber) {
    return this.contactNumberPipe.transform(contactNumber);
  }

  public async confirmCallContact(contactNumber: string) {
    const alert = await this.alertController.create({
      header: 'Confirm Call',
      message: `Call ${this.userName} ?`,
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
          handler: (blah) => {
            console.log('Confirm Cancel: blah');
          }
        }, {
          text: 'Call',
          handler: () => {
            this.callContact(contactNumber);
          }
        }
      ]
    });

    await alert.present();
  }

  closeModal() {
    this.modalController.dismiss();
  }

  ngOnInit() {
    this.contactDetails.forEach(cd => {
      const contactType = cd.contactDetailTypeName;

      if (cd.telephoneNumber !== null) {
        this.contacts.push
          ({description: `${contactType} - Telephone`, contactNumber: cd.telephoneNumber});
      }

      if (cd.mobileNumber !== null) {
        this.contacts.push
          ({description: `${contactType} - Mobile`, contactNumber: cd.mobileNumber});
      }
    });
  }
}
