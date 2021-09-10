import { TreeModel } from '../_interfaces/tree/treeModel';
import { ToyModel } from '../_interfaces/toy/toyModel';
import { BasketModel } from '../_interfaces/basketModel';
import { PriceAndSizeModel } from '../_interfaces/tree/priceAndSizeModel';
import { RepositoryService } from '../shared/services/repository.service';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../shared/services/authentication.service';
import { Router, ActivatedRoute, NavigationExtras} from '@angular/router';
import { TreeForBasketModel } from "../_interfaces/tree/treeForBasketModel";
import { ToyForBasketModel } from "../_interfaces/toy/toyForBasketModel";

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  public basket: BasketModel;
  public goodList: string[];
  public tree: TreeModel;
  public toy: ToyModel;
  public trees: TreeForBasketModel[];
  public toys: ToyForBasketModel[];
  public priceAndSizeModels: PriceAndSizeModel[];
  public errorMessage = '';
  public showError: boolean;
  public id : string;
  public isUserAuthenticated: boolean;
  public isUserAdmin : boolean;
  public p : PriceAndSizeModel;
  public basketId: string;
  showBasket: boolean;
  spin: boolean;

  constructor(
    private repository: RepositoryService,
    private authService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router)
    {
    this.isUserAuthenticated = this.authService.isUserAuthenticated();
    if(this.isUserAuthenticated)
    {
      this.isUserAdmin = this.authService.isUserAdmin();
    }
   }

  ngOnInit() {
    this.spin = true;
    this.showBasket = false;
    this.trees = new Array();
    this.toys = new Array();
    this.getBasket();  
  }

  public getBasket= () => {
    this.showBasket = true;
    const apiAddress = 'api/baskets';
    this.repository.getData(apiAddress)
      .subscribe(res => {
      this.spin = false;
        this.basket = res as BasketModel;
        this.trees = this.basket.trees;
        this.toys = this.basket.toys;
        if(this.trees.length===0&&this.toys.length===0)
        {
          this.showBasket = false;
        }
        this.basketId = this.basket.id;
      });
      
  }

  public deleteGood=(id:string)=>{
    var result = confirm("Впевнений, що хочеш видалити?");
    if (result) {
    const apiAddress = 'api/baskets/deleteGoodFromBasket?detailsId='+id;
    this.repository.delete(apiAddress)
      .subscribe(()=>this.getBasket()); 
    }
  }

  public orderTree=(id:string)=>{
    this.showError = false;
    var address = '/good/tree/order-tree/';
    this.router.navigate([address,id]);
  }

  public orderToy=(id:string)=>{
    this.showError = false;
    var address = '/good/toy/order-toy/';
    this.router.navigate([address,id]);
  }
  
  public createImgPath = (serverPath: string) => {
    return this.repository.createImgPath(serverPath);
  }

  public minPrice=(priceAndSizeModels)=>{
    var prices = Array();
    priceAndSizeModels.forEach(element => {
      prices.push(element.price);  
    });
    return Math.min.apply(null,prices);
  }

}
