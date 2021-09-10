import { PaymentModel } from '../paymentModel';
import { DeliveryModel } from '../deliveryModel';
import { TreeForBasketModel } from "../tree/treeForBasketModel";
import { ToyForBasketModel } from "../toy/toyForBasketModel";

export interface OrderModel {
    //id: string;
    user:string;
    userEmail:string;
    firstName: string;
    lastName: string;
    address: string;
    phone: string;
    trees:TreeForBasketModel[];
    toys:ToyForBasketModel[];
    deliveries:DeliveryModel[];
    payments:PaymentModel[];
    date: Date;
 }