import { AmhpListService } from './amhp-list.service';
import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

describe('AmhpListService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: AmhpListService = TestBed.get(AmhpListService);
    expect(service).toBeTruthy();
  });
});
