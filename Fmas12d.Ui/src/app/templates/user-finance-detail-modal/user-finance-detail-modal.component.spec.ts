import { UserFinanceDetailModalComponent } from './user-finance-detail-modal.component';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

describe('UserFinanceDetailModalComponent', () => {
  let component: UserFinanceDetailModalComponent;
  let fixture: ComponentFixture<UserFinanceDetailModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserFinanceDetailModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserFinanceDetailModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
