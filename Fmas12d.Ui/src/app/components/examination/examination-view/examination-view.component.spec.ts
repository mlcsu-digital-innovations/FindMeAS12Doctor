import { ActivatedRoute } from '@angular/router';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ExaminationViewComponent } from './examination-view.component';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { RouterService } from 'src/app/services/router/router.service';
import { SharedComponentsModule } from '../../shared-components.module';

describe('ExaminationViewComponent', () => {
  let component: ExaminationViewComponent;
  let fixture: ComponentFixture<ExaminationViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        ExaminationViewComponent
      ],
      imports: [
        HttpClientModule,
        SharedComponentsModule
      ],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            paramMap: of({referralId: 1})
          }
        },
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
    fixture = TestBed.createComponent(ExaminationViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
