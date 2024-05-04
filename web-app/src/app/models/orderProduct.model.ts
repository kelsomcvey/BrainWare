export class OrderProduct {
    public orderId: number;

    public productId: number;
    public productName: string;
    public quantity: number;
    public price: number;

    constructor(
        orderId: number,
        productId: number,
        productName: string,
        quantity: number,
        price: number
    ) {

        this.orderId = orderId;
        this.productId = productId;
        this.productName = productName;
        this.quantity = quantity;
        this.price = price;

    }
}