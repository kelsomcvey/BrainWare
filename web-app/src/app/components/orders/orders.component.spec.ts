import { ComponentFixture, TestBed } from '@angular/core/testing';
import { OrdersComponent } from '../orders/orders.component';
import { CommonModule } from '@angular/common';
import { OrdersService } from '../../services/orders.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('OrdersComponent', () => {
  let component: OrdersComponent;
  let fixture: ComponentFixture<OrdersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({   
      imports: [CommonModule, HttpClientTestingModule],
      providers: [OrdersService]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

});
