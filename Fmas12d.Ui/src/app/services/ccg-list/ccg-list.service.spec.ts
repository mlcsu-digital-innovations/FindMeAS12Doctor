import { CcgListService } from './ccg-list.service';
import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

describe('CcgListService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: CcgListService = TestBed.get(CcgListService);
    expect(service).toBeTruthy();
  });
});
