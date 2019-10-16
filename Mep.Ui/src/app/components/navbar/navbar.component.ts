import { Component, OnInit } from '@angular/core';
import { version } from '../../../../package.json';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  // Add standard navbar options here !

  public version: string = version;

  constructor() { }

  ngOnInit() {
    console.log(version);
  }

}
