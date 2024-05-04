import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Order } from "../models/order.model";
import { Company } from "../models/company.model";

@Injectable({
    providedIn: 'root'
})

export abstract class IOrdersService {

    constructor() {}

    public abstract getOrders(orderNumber: number): Observable<Order[]>;
    public abstract getCompanies(): Observable<Company[]>
}