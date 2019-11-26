import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router, NavigationEnd } from '@angular/router';
import { RouterService } from './router.service';
import { TestBed, inject, ComponentFixture } from '@angular/core/testing';

class MockRouter {
  // events = jasmine.createSpy('events');

  public ne = new NavigationEnd(0, 'http://localhost:4200/dashboard', 'http://localhost:4200/dashboard');

  public url = 'URL';

  public events = new Observable(observer => {
    observer.next(this.ne);
    observer.complete();
  });
}

describe('RouterService', () => {

  const router: MockRouter = new MockRouter();

  beforeEach(() => {
    TestBed.configureTestingModule(
    {
      declarations: [
      ],
      imports: [
        HttpClientModule
      ],
      providers: [
        {
          provide: Router,
          useValue: router
        }
      ]
    });

  }
  );

  it('should be created', (() => {
    const service: RouterService = TestBed.get(RouterService);
    expect(service).toBeTruthy();
  }));

});
