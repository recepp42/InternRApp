import { TestBed } from '@angular/core/testing';

import { AcceptHeaderInterceptor } from './accept-header.interceptor';

describe('AcceptHeaderInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      AcceptHeaderInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: AcceptHeaderInterceptor = TestBed.inject(AcceptHeaderInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
