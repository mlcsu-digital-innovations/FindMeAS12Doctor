import { HttpClientModule } from '@angular/common/http';
import { of, Observable } from 'rxjs';
import { Router, NavigationEnd } from '@angular/router';
import { RouterService } from './router.service';
import { TestBed } from '@angular/core/testing';

class MockRouter {
  // events = jasmine.createSpy('events');

  public ne = new NavigationEnd(0, 'http://localhost:4200/dashboard', 'http://localhost:4200/dashboard');

  public url = 'URL';

  public events = new Observable(observer => {
    observer.next(this.ne);
    observer.complete();
  });
}

let router: Router;

describe('RouterService', () => {

  beforeAll(() => {
    console.log('Create the service');
    router = jasmine.createSpyObj('Router', ['events']);
  });

  beforeEach(() => TestBed.configureTestingModule(
    {
      declarations: [
      ],
      imports: [
        HttpClientModule
      ],
      providers: [
        {
          provide: Router,
          useValue: MockRouter
        }
      ]
    })
  );



  it('should be created', () => {
    const service: RouterService = TestBed.get(RouterService)(router);
    console.log(service);
    expect(service).toBeTruthy();
  });
});
