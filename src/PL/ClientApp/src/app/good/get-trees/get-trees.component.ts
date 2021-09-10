import { TreeModel } from '../../_interfaces/tree/treeModel';
import { PriceAndSizeModel } from '../../_interfaces/tree/priceAndSizeModel';
import { RepositoryService } from '../../shared/services/repository.service';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { ActivatedRoute, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import {Event} from '@angular/router';
import { Options, LabelType } from "@angular-slider/ngx-slider";
import { RecomendationDto } from 'src/app/_interfaces/recomendationDto';


@Component({
  selector: 'app-tree',
  templateUrl: './get-trees.component.html',
  styleUrls: ['./get-trees.component.css']
})
export class GetTreeComponent implements OnInit {
  public trees: TreeModel[];
  public isUserAuthenticated: boolean;
  public isUserAdmin : boolean;
  public p : PriceAndSizeModel;
  spin:boolean;
  treeType: string;
  minValue: number = 0;
  maxValue: number = 50000;
  min: number;
  max: number;
  public errorMessage = '';
  public showError: boolean;
  public errorMessageForRecomendations = '';
  public showErrorForRecomendations: boolean;
  public showFiltersBool:boolean;
  options: Options = {
    floor: 0,
    ceil: 50000,
    translate: (value: number, label: LabelType): string => {
      switch (label) {
        case LabelType.Low:
          this.min = value;
        case LabelType.High:
          this.max = value;
        default:
          return "";
      }
    }
  };
  colors: string[];
  sizes: number[];
  treesForView: TreeModel[];
  isEmpty: boolean;

  constructor(private repository: RepositoryService,    
    private route: ActivatedRoute,
     private authService: AuthenticationService,
     private router: Router) {
      this.router.events.subscribe((event: Event) => {
        this.spin = true;
        if (event instanceof NavigationStart) {
          
        }

        if (event instanceof NavigationEnd) {
          
          this.getTrees();
        }
      }
      );
    this.isUserAuthenticated = this.authService.isUserAuthenticated();
    if(this.isUserAuthenticated)
    {
      this.isUserAdmin = this.authService.isUserAdmin();
    }
   }

  ngOnInit() {
    this.getTrees();
    this.windowWidth();
    this.showError = false;
    this.showErrorForRecomendations = false;
    this.colors = new Array() as string[];
    this.sizes = new Array() as number[];
  }

  showFilters(){
    this.showFiltersBool = !this.showFiltersBool;
  }

  addColor(color:string) {
    if (!this.colors.some(a => a === color)) {
      this.colors.push(color);
    }
    else{
      this.colors = this.colors.filter(function(value){ 
        return value !== color;
      });
    }
  }


  addSize(size:number) {
    if (!this.sizes.some(a => a === size)) {
      this.sizes.push(size);
    }
    else{
      this.sizes = this.sizes.filter(function(value){ 
        return value !== size;
      });
    }
  }

  getFilteredTrees=()=>{
    this.showErrorForRecomendations = false;
    this.showError = false;    
    var treesFilteredByAllColor = new Array() as TreeModel[];
    if(this.colors.length==0)
    {
      treesFilteredByAllColor = this.trees as TreeModel[];
    }
    else
    {
      this.colors.forEach(x=>{
        var treesFilteredByOneColor = this.trees.filter(c=>c.color===x);
        treesFilteredByOneColor.forEach(tree=>{
          treesFilteredByAllColor.push(tree);
        });
      })
    }
    
    var treeFilteredByAllSizesAndPrices = new Array() as TreeModel[];
    treesFilteredByAllColor.forEach(t=>{
      var treePricesAndSizes = t.priceAndSizeModels;
      if(this.sizes.length===0){
        if(treePricesAndSizes.some(x=>x.price>=this.minValue&&x.price<=this.maxValue) )
        {
          treeFilteredByAllSizesAndPrices.push(t);
        }
      }
      else{
        for(let s of this.sizes) {
          if(treePricesAndSizes.some(x=>x.size===s&&x.price>=this.minValue&&x.price<=this.maxValue) )
          {
            treeFilteredByAllSizesAndPrices.push(t);
            break;
          }
       }
      }
    })
    this.treesForView = treeFilteredByAllSizesAndPrices;
    if(this.treesForView.length===0)
    this.showError=true;
    this.errorMessage = "Таких ялинок немає";
  }

  public getTrees = () => {
    this.showErrorForRecomendations = false;
    this.showError = false;
    this.treeType = this.route.snapshot.paramMap.get('treeType')!;
    const apiAddress = 'api/trees?treeType='+this.treeType;
    this.repository.getData(apiAddress)
      .subscribe(res => {
        var prices = new Array();
        this.trees = res as TreeModel[];
        if(this.trees.length==0)
        {
          this.isEmpty=true;
          this.spin = false;
        }
        this.treesForView = res as TreeModel[];
        this.spin = false;
      }),
      ()=>{
        this.isEmpty=true;
        this.spin = false;
      };
  }

  public windowWidth =()=>{
    if(window.innerWidth>700){
      this.showFiltersBool = true;
      return this.showFiltersBool;
    }
    else{
      this.showFiltersBool = false;
    return this.showFiltersBool;}
  }

  public minPrice=(priceAndSizeModels)=>{
    var prices = Array();
    priceAndSizeModels.forEach(element => {
      prices.push(element.price);  
    });
    return Math.min.apply(null,prices);
  }

  public deleteTree=(id: string)=>{
    var result = confirm("Впевнений, що хочеш видалити?");
    if (result) {
    this.showError = false;
    const apiAddress = 'api/trees/delete?id='+id;
    this.repository.delete(apiAddress)
      .subscribe(()=>{
        this.spin = true;
        this.getTrees();
      }),
      ()=>{

        this.showError = true;
        this.errorMessage = 'Неможливо видалити, спробуйте пізніше';
      };   
    } 
  }

  public addToRecomendations=(id)=>{
    this.showErrorForRecomendations = false;
    const apiAddress = 'api/SearchAndRecomendationResponse/addRecomendation';
    const recomendation: RecomendationDto = {
      id: "00000000-0000-0000-0000-000000000000",
      goodId: id,
      goodType: "ялинка"
    };
    this.repository.create(apiAddress,recomendation)
      .subscribe(_=>{
        this.router.navigate(['/home']);
      },
      error=>{
        this.errorMessageForRecomendations = error;
        this.showErrorForRecomendations = true;
      }     
      );
  }

  public updateTree=(id: string)=>{
    const apiAddress ='/good/tree/update-tree/'+id;
    this.router.navigate([apiAddress]);
  }

  public addTree=()=>{
    this.router.navigate(['/good/tree/add-tree/new']);
  }

  public createImgPath = (serverPath: string) => {
    return this.repository.createImgPath(serverPath);
  }
}
