import { ActivatedRoute } from '@angular/router';
import { async, ComponentFixture, TestBed } from 'src/app/components/assessment/assessment-list/node_modules/src/app/components/assessment/assessment-create/node_modules/@angular/core/testing';
import { AssessmentViewComponent } from './assessment-view.component';
import { HttpClientModule } from 'src/app/components/assessment/assessment-create/node_modules/@angular/common/http';
import { of } from 'src/app/components/assessment/assessment-create/node_modules/rxjs';
import { RouterService } from 'src/app/services/router/router.service';
import { SharedComponentsModule } from '../../shared-components.module';

describe('AssessmentViewComponent', () => {
  let component: AssessmentViewComponent;
  let fixture: ComponentFixture<AssessmentViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AssessmentViewComponent
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
    fixture = TestBed.createComponent(AssessmentViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
