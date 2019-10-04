import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ReferralCreateComponent } from './referral-create.component';
import { SharedComponentsModule } from '../../shared-components.module';
import { Router } from '@angular/router';
import { of } from 'rxjs';

describe('ReferralCreateComponent', () => {
  let component: ReferralCreateComponent;
  let fixture: ComponentFixture<ReferralCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        ReferralCreateComponent
    ],
      imports: [
        HttpClientModule,
        NgbModule,
        ReactiveFormsModule,
        SharedComponentsModule
      ],
      providers: [
        {
          provide: Router,
          useValue: {
            paramMap: of({referralId: 1})
          }
        }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferralCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
