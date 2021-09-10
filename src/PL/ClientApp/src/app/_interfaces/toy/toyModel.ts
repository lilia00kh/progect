import { ImageModel } from "../imageModel";

export interface ToyModel {
    id: string;
    name: string;
    description: string;
    price: number;
    imageModels: ImageModel[];
 }