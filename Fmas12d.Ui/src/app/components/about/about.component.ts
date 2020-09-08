import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  services: string[] = [
    'Research and Development',
    'Advisory Services',
    'Business Analysis',
    'Evidence Review',
    'Digital Strategy Development',
    'Consensus Building  Evaluation',
    'Leadership',
    'Transformational Change',
    'Proof of Concept',
    'Agile Software Development',
    'Implementation'
  ];

constructor() {}

ngOnInit() {

  }
}
