import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { PostcodeValidationService } from './postcode-validation.service';

describe('PostcodeValidationService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: PostcodeValidationService = TestBed.get(PostcodeValidationService);
    expect(service).toBeTruthy();
  });
});
