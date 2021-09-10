import { TreeModel } from '../_interfaces/tree/treeModel';
import { RepositoryService } from '../shared/services/repository.service';
import { Component, OnInit } from '@angular/core';
import { SearchAndRecomendationResponseModel } from '../_interfaces/SearchAndRecomendationResponseModel';
import { ToyModel } from '../_interfaces/toy/toyModel';
import { AuthenticationService } from '../shared/services/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public homeText: string;
  recomendations: SearchAndRecomendationResponseModel;
  trees: TreeModel[];
  toys: ToyModel[];
  isUserAuthenticated: boolean;
  isUserAdmin: boolean;
  spin: boolean;
  showError: boolean;
  errorMessage: string;

  constructor(
    private repository: RepositoryService,
    private authService: AuthenticationService,) {
    this.isUserAuthenticated = this.authService.isUserAuthenticated();
    this.spin = true;
    if(this.isUserAuthenticated)
    {
      this.isUserAdmin = this.authService.isUserAdmin();
    }
   }

  ngOnInit(): void {
    this.spin = true;
    this.homeText = "Вітаю на сайті Ярка, хи хи хи :)";
    this.getRecomendations();
  }

  public getRecomendations = () => {
    const apiAddress = 'api/SearchAndRecomendationResponse/getAllRecomendations';
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.recomendations = res as SearchAndRecomendationResponseModel;
        this.trees = this.recomendations.trees as TreeModel[];
        this.toys = this.recomendations.toys as ToyModel[];
        if(this.trees.length==0&&this.toys.length==0)
        {
          this.showError = true;
          this.errorMessage = 'Список рекомендацій порожній'
        }
        this.spin = false;
      },
      error =>{
      });
  }

  
  public minPrice=(priceAndSizeModels)=>{
    var prices = Array();
    priceAndSizeModels.forEach(element => {
      prices.push(element.price);  
    });
    return Math.min.apply(null,prices);
  }

  public createImgPath = (serverPath: string) => {
    return this.repository.createImgPath(serverPath);
  }

  public deleteRecomendation=(id: string)=>{
    var result = confirm("Впевнений, що хочеш видалити?");
    if (result) {
      const apiAddress = 'api/SearchAndRecomendationResponse/deleteRecomendation?id='+id;
      this.repository.delete(apiAddress)
        .subscribe(()=>{
          this.spin = true;
          this.getRecomendations();
        }); 
      }  
  }

}
