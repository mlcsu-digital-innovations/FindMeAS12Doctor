import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Component } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReferralListComponent } from './referral-list.component';
import { ReferralListService } from 'src/app/services/referral-list/referral-list-service';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedComponentsModule } from '../../shared-components.module';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  template: ''
})
class DummyComponent {
}

describe('ReferralListComponent', () => {
  let component: ReferralListComponent;
  let fixture: ComponentFixture<ReferralListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        DummyComponent,
        ReferralListComponent
      ],
      imports: [
        HttpClientModule,
        RouterTestingModule.withRoutes([
          {path: 'patient/edit/:patientId', component: DummyComponent}
        ]),
        SharedComponentsModule
      ],
      providers: [
        DecimalPipe,
        ReferralListService,
        ToastService,
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferralListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
