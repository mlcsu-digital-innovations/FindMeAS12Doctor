import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-danger-toast',
  templateUrl: './danger-toast.component.html',
  styleUrls: ['./danger-toast.component.css']
})
export class DangerToastComponent implements OnInit{

  @Input()
  toastMessage: string;

ngOnInit() {
  console.log(this.toastMessage);
}


}
