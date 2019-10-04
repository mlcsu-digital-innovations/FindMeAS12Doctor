import { HttpClientModule } from '@angular/common/http';
import { NameIdListService } from './name-id-list.service';
import { TestBed } from '@angular/core/testing';

describe('NameIdListService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: NameIdListService = TestBed.get(NameIdListService);
    expect(service).toBeTruthy();
  });
});
