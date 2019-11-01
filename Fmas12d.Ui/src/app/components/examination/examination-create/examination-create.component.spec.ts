import { ActivatedRoute } from '@angular/router';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ExaminationCreateComponent } from './examination-create.component';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { SharedComponentsModule } from '../../shared-components.module';
import { RouterService } from 'src/app/services/router/router.service';

describe('ExaminationCreateComponent', () => {
  let component: ExaminationCreateComponent;
  let fixture: ComponentFixture<ExaminationCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        ExaminationCreateComponent
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
    fixture = TestBed.createComponent(ExaminationCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
