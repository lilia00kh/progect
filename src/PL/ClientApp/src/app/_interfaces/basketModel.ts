import { TreeForBasketModel } from "./tree/treeForBasketModel";
import { ToyForBasketModel } from "./toy/toyForBasketModel";

export interface BasketModel {
    id: string;
    userName: string;
    trees: TreeForBasketModel[];
    toys: ToyForBasketModel[];
 }