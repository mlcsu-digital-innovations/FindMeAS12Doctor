import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { CcgListService } from './ccg-list.service';

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
