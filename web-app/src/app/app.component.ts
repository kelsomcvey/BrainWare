import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { OrdersService } from './services/orders.service';
import { Order } from './models/order.model';
import { OrdersComponent } from './components/orders/orders.component';

@Component({
  standalone: true,
  imports: [CommonModule, OrdersComponent],
  selector: 'web-app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  orders: Order[] = [];
  year = new Date().getFullYear();


  constructor() {
  }

  ngOnInit(): void {
   // this.getOrders();
  }

 
}

