import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IOrdersService } from './i-orders.service';
import { Order } from '../models/order.model';
import { OrderProduct } from '../models/orderProduct.model';
import { Company } from '../models/company.model';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrdersService implements IOrdersService {
  private apiUrl: string = environment.apiUrl + 'order/';

  constructor(private http: HttpClient) { }

  public getOrders(companyNumber: number): Observable<Order[]> {
    const httpOptions = this.getHttpOptions();
    return this.http.get<any[]>(this.apiUrl + companyNumber.toString(), httpOptions).pipe(
      map((resp: any) => {
        const orders: Order[] = [];
        resp.forEach((order: any) => {
          const products: OrderProduct[] = [];
          order.orderProducts.forEach((product: any) => {
            products.push(new OrderProduct(product.orderId, product.productId, product.productName, product.quantity, product.price));
          });
          const newOrder = new Order(order.orderId, order.companyName, order.description, order.orderTotal, products);
          orders.push(newOrder);
        });
        return orders;
      }), catchError(error => {
        //console.error('Error fetching orders:', error);
        return throwError(() => 'Error fetching orders');
      })
    );
  }


  public getCompanies(): Observable<Company[]> {
    const httpOptions = this.getHttpOptions();
    return this.http.get<any[]>(this.apiUrl + "companies", httpOptions).pipe(
      map((resp: any) => {
        const companies: Company[] = [];
        resp.forEach((c: any) => {

          const company = new Company(c.companyId, c.companyName);
          companies.push(company);
        });
        return companies;
      }), catchError(error => {
    
        return throwError(() => 'Error fetching companies');
      })
    );
  }


  private getHttpOptions(): any {
    return new HttpHeaders({
      'Content-Type': 'application/json'
    });
  }
}