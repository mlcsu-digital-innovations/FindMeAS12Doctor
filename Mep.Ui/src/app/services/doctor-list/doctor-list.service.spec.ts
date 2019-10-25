import { DoctorListService } from './doctor-list.service';
import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

describe('DoctorListService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [
      HttpClientModule
    ]
  }));

  it('should be created', () => {
    const service: DoctorListService = TestBed.get(DoctorListService);
    expect(service).toBeTruthy();
  });
});
