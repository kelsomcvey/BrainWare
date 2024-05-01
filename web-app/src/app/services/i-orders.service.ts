import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export abstract class IOrdersService {

    constructor() {}

    public abstract getOrders(): Observable<any[]>;
}