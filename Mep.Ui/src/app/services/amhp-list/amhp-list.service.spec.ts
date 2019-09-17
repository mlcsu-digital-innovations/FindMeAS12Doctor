import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { AmhpListService } from './amhp-list.service';

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
