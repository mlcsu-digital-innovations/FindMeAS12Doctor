import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { AvailableDoctor } from 'src/app/interfaces/available-doctor';
import { CONTACT_DETAIL_TYPE_HOME, CONTACT_DETAIL_TYPE_BASE } from 'src/app/constants/Constants';
import { ContactNumberPipe } from 'src/app/pipes/contact-number.pipe';

@Component({
  selector: 'app-contact-detail-modal',
  templateUrl: './contact-detail.modal.component.html',
  styleUrls: ['./contact-detail.modal.component.css'],
  providers: [ContactNumberPipe]
})
export class ContactDetailModalComponent implements OnInit {

  public contacts: {contactDescription: string, contactNumber: string}[] = [];

  @Input() public contact: AvailableDoctor;

  constructor(
    private activeModal: NgbActiveModal,
    private contactNumberPipe: ContactNumberPipe
  ) { }

  closeModal() {
    this.activeModal.close();
  }

  transformNumber(contactNumber: string) {
    return this.contactNumberPipe.transform(contactNumber);
  }

  ngOnInit() {
    this.contact.contactDetails.forEach(contact => {

      let contactDescription = '';

      switch (contact.contactDetailTypeId) {
        case CONTACT_DETAIL_TYPE_BASE:
          contactDescription = 'Base';
          break;
        case CONTACT_DETAIL_TYPE_HOME:
          contactDescription = 'Home';
          break;
      }

      if (contact.telephoneNumber !== null && contact.telephoneNumber !== '') {
        this.contacts.push(
          {
            contactDescription: `${contactDescription} - Telephone`,
            contactNumber: contact.telephoneNumber
          }
        );
      }

      if (contact.mobileNumber !== null && contact.mobileNumber !== '') {
        this.contacts.push(
          {
            contactDescription: `${contactDescription} - Mobile`,
            contactNumber: contact.mobileNumber
          }
        );
      }

    });
  }
}
