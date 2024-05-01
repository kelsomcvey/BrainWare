import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { IOrdersService } from './i-orders.service';

export interface Order {
  // Define the properties of your order object here based on your API response
  // (e.g., id, name, date, etc.)
}

@Injectable({
  providedIn: 'root'
})
export class OrdersService implements IOrdersService {
  private apiUrl: string = 'https://localhost:44347/api/order/1'; // Replace with your actual API URL

  constructor(private http: HttpClient) {}

  public getOrders(): Observable<any[]> {
    const httpOptions = this.getHttpOptions();
    return this.http.get<any[]>(this.apiUrl,  httpOptions).pipe(
        map((resp: any) => {
            console.log(resp);
            let mappedVal = resp;
            return mappedVal;
        })
    );
  }

  private getHttpOptions(): any {
    return new HttpHeaders({
      'Content-Type': 'application/json'
    });
  }
}