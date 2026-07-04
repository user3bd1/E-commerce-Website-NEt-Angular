import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestErrorComponent } from './test-error.component';

describe('TestErrorComponent', () => {
  let component: TestErrorComponent;
  let fixture: ComponentFixture<TestErrorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TestErrorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestErrorComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
