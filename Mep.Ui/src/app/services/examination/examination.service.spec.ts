import { ExaminationService } from './examination.service';
import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

describe('ExaminationService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: ExaminationService = TestBed.get(ExaminationService);
    expect(service).toBeTruthy();
  });
});
