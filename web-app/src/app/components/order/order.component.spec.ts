import { TestBed } from '@angular/core/testing';
import { OrderComponent } from './order.component';

describe('OrdersComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrderComponent],
    }).compileComponents();
  });

  it('should render total', () => {
    const fixture = TestBed.createComponent(OrderComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('p')?.textContent).toContain(
      'Total'
    );
  });

  
});
