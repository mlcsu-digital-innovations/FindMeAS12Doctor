import { Component, OnInit } from '@angular/core';
import { HelpService } from 'src/app/services/help/help.service';
import { Observable } from 'src/app/pages/amhp-assessment-list/node_modules/rxjs';
import { version } from '../../../../package.json';

@Component({
  selector: 'app-help',
  templateUrl: './help.page.html',
  styleUrls: ['./help.page.scss'],
})
export class HelpPage implements OnInit {
  public apiVersion$: Observable<string>;
  public appVersion: string = version;

  constructor(private help: HelpService) { }

  ngOnInit() {
    this.apiVersion$ = this.help.getApiVersion();
  }

}
