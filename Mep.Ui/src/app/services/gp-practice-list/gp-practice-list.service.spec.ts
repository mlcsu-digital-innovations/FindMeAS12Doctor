import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { GpPracticeListService } from './gp-practice-list.service';

describe('GpPracticeListService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: GpPracticeListService = TestBed.get(GpPracticeListService);
    expect(service).toBeTruthy();
  });
});
