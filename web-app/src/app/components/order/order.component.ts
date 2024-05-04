import { CommonModule } from "@angular/common";
import { Component, Input, OnInit } from "@angular/core";
import { Order } from "src/app/models/order.model";

@Component({
    standalone: true,
    imports: [CommonModule],
    selector: 'bw-app-order',
    templateUrl: './order.component.html',
    styleUrls: ['./order.component.scss']
})

export class OrderComponent implements OnInit {

    @Input() order: Order | undefined;

    constructor() { }

    ngOnInit(): void {
        
    }
}