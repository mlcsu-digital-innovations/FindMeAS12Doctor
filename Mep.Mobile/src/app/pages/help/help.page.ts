import { Component, OnInit } from '@angular/core';
import { version } from '../../../../package.json';
import { Observable } from 'rxjs';
import { HelpService } from 'src/app/services/help.service/help.service';

@Component({
  selector: 'app-help',
  templateUrl: './help.page.html',
  styleUrls: ['./help.page.scss'],
})
export class HelpPage implements OnInit {
  public appVersion: string = version;
  public apiVersion$: Observable<string>;

  constructor(private help: HelpService) { }

  ngOnInit() {
    this.apiVersion$ = this.help.getApiVersion();
  }

}
