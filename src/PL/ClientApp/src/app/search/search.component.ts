import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { RepositoryService } from '../shared/services/repository.service';
import { SearchAndRecomendationResponseModel } from '../_interfaces/SearchAndRecomendationResponseModel';
import {Event} from '@angular/router';
import { TreeModel } from '../_interfaces/tree/treeModel';
import { ToyModel } from '../_interfaces/toy/toyModel';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  showError: boolean;
  searchName: string;
  trees: TreeModel[];
  toys: ToyModel[];
  searchRes: SearchAndRecomendationResponseModel;
  errorMessage: string;
  showTrees: boolean;
  showToys: boolean;

  constructor(
    private repository: RepositoryService,    
    private route: ActivatedRoute,
    private router: Router) {
      this.router.events.subscribe((event: Event) => {
        if (event instanceof NavigationStart) {
          
        }

        if (event instanceof NavigationEnd) {
          
          this.getTreesAndToysWithName();
        }
      }
      );
    }

  ngOnInit() {
    this.showToys = true;
    this.showTrees = true;
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

  public getTreesAndToysWithName = () => {
    this.showToys = true;
    this.showTrees = true;
    this.showError = false;
    this.searchName = this.route.snapshot.paramMap.get('name')!;
    const apiAddress = 'api/SearchAndRecomendationResponse/SearchByName?name='+this.searchName;
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.searchRes = res as SearchAndRecomendationResponseModel;
        if(this.searchRes.trees==null&&this.searchRes.toys==null)
        {
          this.showTrees = false;
          this.showToys = false;
          this.showError = true;
          this.errorMessage = "Товару з такою назвою немає."
          return;
        }
        if(this.searchRes.trees.length===0)
        {
          this.showTrees = false;
        }
        if(this.searchRes.toys.length===0)
        {
          this.showToys = false;
        }
        this.trees = this.searchRes.trees as TreeModel[];
        this.toys = this.searchRes.toys as ToyModel[];
      });
  }

}
