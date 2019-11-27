import { Component, Input, Output, EventEmitter } from '@angular/core';
import { UnregisteredUser } from 'src/app/interfaces/unregistered-user';

@Component({
  selector: 'app-unregistered-users-modal',
  templateUrl: './unregistered-users-modal.component.html',
  styleUrls: ['./unregistered-users-modal.component.css']
})
export class UnregisteredUsersModalComponent {

  @Input()
  unregisteredUsers: UnregisteredUser[];

  @Output() selected = new EventEmitter<UnregisteredUser>();
  @Output() cancelled = new EventEmitter();

  selectAction(user: UnregisteredUser) {
    this.selected.emit(user);
  }

  cancelAction() {
    this.cancelled.emit();
  }
}
