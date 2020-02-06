import { ContactDetailTypeService } from './contact-detail-type.service';
import { TestBed } from '@angular/core/testing';

describe('ContactDetailTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ContactDetailTypeService = TestBed.get(ContactDetailTypeService);
    expect(service).toBeTruthy();
  });
});
