import { TreeModel } from "./tree/treeModel";
import { ToyModel } from "./toy/toyModel";

export interface SearchAndRecomendationResponseModel
{
    trees:TreeModel[];
    toys:ToyModel[];
}