import { OrderProduct } from "./orderProduct.model";

export class Order {
    public orderId: number;
    public companyName: string;
    public description: string;
    public orderTotal: number;
    public orderProducts: OrderProduct[];

    constructor(orderId: number,
        companyName: string,
        description: string,
        orderTotal: number,
        orderProducts: OrderProduct[]) {

        this.orderId = orderId;
        this.companyName = companyName;
        this.description = description;
        this.orderTotal = orderTotal;
        this.orderProducts = orderProducts;

    }
}