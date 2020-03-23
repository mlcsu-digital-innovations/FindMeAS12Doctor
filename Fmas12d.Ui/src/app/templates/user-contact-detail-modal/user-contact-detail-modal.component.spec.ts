import { UserContactDetailModalComponent } from './user-contact-detail-modal.component';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

describe('UserContactDetailModalComponent', () => {
  let component: UserContactDetailModalComponent;
  let fixture: ComponentFixture<UserContactDetailModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserContactDetailModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserContactDetailModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
