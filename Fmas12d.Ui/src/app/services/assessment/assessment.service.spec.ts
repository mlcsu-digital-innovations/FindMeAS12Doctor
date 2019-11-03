import { AssessmentService } from './assessment.service';
import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

describe('AssessmentService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: AssessmentService = TestBed.get(AssessmentService);
    expect(service).toBeTruthy();
  });
});
