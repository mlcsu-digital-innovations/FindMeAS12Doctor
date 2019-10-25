import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { of } from 'rxjs';
import { ReactiveFormsModule } from '@angular/forms';
import { ReferralCreateComponent } from './referral-create.component';
import { Router } from '@angular/router';
import { RouterService } from 'src/app/services/router/router.service';
import { SharedComponentsModule } from '../../shared-components.module';

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
          provide: RouterService,
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
