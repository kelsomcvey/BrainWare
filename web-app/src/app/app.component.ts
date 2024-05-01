import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { OrdersService } from './services/orders.service';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule],
  selector: 'web-app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  orders: any[] = [];
  year = new Date().getFullYear();
  endpoint: string = 'https://localhost:44347/api/order/1';

  constructor(private orderService : OrdersService) {
  }

  ngOnInit(): void {
    this.getOrders();
  }

 getOrders(): any {
  this.orderService.getOrders().subscribe(val => {
    console.log(val);
  })
 }
}

