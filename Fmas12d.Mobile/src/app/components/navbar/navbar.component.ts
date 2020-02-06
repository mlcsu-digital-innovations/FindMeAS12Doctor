import { Component, OnInit, ChangeDetectorRef, Input } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { AlertController } from '@ionic/angular';
import { Location } from '@angular/common';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  public connection: boolean;

  @Input()
  title: string;

  @Input()
  showWarning: boolean;

  @Input()
  lastUpdated: Date;

  constructor(
    private networkService: NetworkService,
    private changeRef: ChangeDetectorRef,
    public alertCtrl: AlertController,
    private location: Location
  ) { }

  ngOnInit() {
    this.connection = this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Online;

    this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {
      this.connection = status === ConnectionStatus.Online;
      this.changeRef.detectChanges();
    });
  }

  async showConfirm() {

    if (this.showWarning === true) {

      const confirm = await this.alertCtrl.create({
        header: 'Confirm!',
        message: 'Your changes have not been saved, are you sure you want to leave?',
        buttons: [
          {
            text: 'Cancel',
            role: 'cancel',
            handler: () => {
            }
          },
          {
            text: 'Okay',
            handler: () => {
              this.location.back();
            }
          }
        ]
      });
      await confirm.present();
    } else {
      this.location.back();
    }
  }

}
