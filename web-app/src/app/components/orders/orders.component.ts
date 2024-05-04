import { Component, OnInit, NgModule } from "@angular/core";
import { OrdersService } from "src/app/services/orders.service";
import { OrderComponent } from "../order/order.component";
import { Order } from "src/app/models/order.model";
import { CommonModule } from "@angular/common";
import { Company } from "src/app/models/company.model";
import { FormsModule } from '@angular/forms'; // Import FormsModule here
import { Observable, throwError } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";
import { LoaderComponent } from "../loader/loader.component";

@Component({
    standalone: true,
    imports: [CommonModule, OrderComponent, FormsModule, LoaderComponent],
    selector: 'bw-app-orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.scss']
})

export class OrdersComponent implements OnInit {
    orders$!: Observable<Order[]>;
    companies$!: Observable<Company[]>;
    companies: Company[] = [];
    selectedCompanyId: number = 1;
    companyDisplayName: string = "";
    loading: boolean = true;

    constructor(private orderService: OrdersService) {
    }

    ngOnInit(): void {
        this.loadCompaniesAndOrders();
      }
    
      loadCompaniesAndOrders(): void {
        this.loading = true;
        this.companies$ = this.orderService.getCompanies().pipe(
          tap(companies => {
            if (companies.length > 0) {
              const firstCompanyId = companies[0].companyId;
              this.companyDisplayName = companies[0].companyName;
              this.getOrders(firstCompanyId);
            } else {
              throw new Error('No companies found.');
            }
          }),
          catchError(error => {
            console.error('Error fetching companies:', error);
            this.loading = false;
            return throwError(() => new Error('test'));
          })
        );
      }
    
      getOrders(companyId: number): void {
        this.orders$ = this.orderService.getOrders(companyId).pipe(
          tap(() => this.loading = false),
          catchError(error => {
            console.error('Error fetching orders:', error);
            this.loading = false;
            return throwError(() => new Error('test'));
          })
        );
      }
    
      onCompanySelected(): void {
        this.loading = true;
       
    
        this.getOrders(this.selectedCompanyId); 
        this.getCompanyNameById(this.selectedCompanyId).subscribe(companyName => {
          this.companyDisplayName = companyName;
        });
      }
      
    
      getCompanyNameById(companyId: number): Observable<string> {
        return this.companies$.pipe(
          map(companies => {
            const foundCompany = companies.find(company => company.companyId == companyId);
           // this.companyDisplayName = foundCompany ? foundCompany.companyName : 'Unknown Company';
            return foundCompany ? foundCompany.companyName : 'Unknown Company';
          })
        );
      }


}