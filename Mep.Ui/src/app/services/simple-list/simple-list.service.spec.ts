import { HttpClientModule } from '@angular/common/http';
import { SimpleListService } from './simple-list.service';
import { TestBed } from '@angular/core/testing';

describe('SimpleListService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: SimpleListService = TestBed.get(SimpleListService);
    expect(service).toBeTruthy();
  });
});
