import { ImageModel } from '../imageModel';
import { PriceAndSizeModel } from './priceAndSizeModel';

export interface TreeModel {
    id: string;
    name: string;
    description: string;
    priceAndSizeModels: PriceAndSizeModel[];
    imageModels: ImageModel[];
    treeType: string;
    color:string;
 }