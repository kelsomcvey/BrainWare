import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { OrdersComponent } from './components/orders/orders.component';

@Component({
  standalone: true,
  imports: [CommonModule, OrdersComponent],
  selector: 'web-app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
 
  year = new Date().getFullYear();
  title = "web-app";

  constructor() {
  }

  ngOnInit(): void {
   // this.getOrders();
  }

 
}

