import { ToyModel } from '../../_interfaces/toy/toyModel';
import { RepositoryService } from '../../shared/services/repository.service';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { Router } from '@angular/router';
import { RecomendationDto } from 'src/app/_interfaces/recomendationDto';


@Component({
  selector: 'app-get-toys',
  templateUrl: './get-toys.component.html',
  styleUrls: ['./get-toys.component.css']
})
export class GetToysComponent implements OnInit {

  public toys: ToyModel[];
  public isUserAuthenticated: boolean;
  public isUserAdmin : boolean;
  public errorMessage = '';
  public showError: boolean;
  spin: boolean;
  isEmpty: boolean;
  
  constructor(
    private repository: RepositoryService,
    private router: Router,
    private authService: AuthenticationService) {
    this.isUserAuthenticated = this.authService.isUserAuthenticated();
    this.spin = true;
    if(this.isUserAuthenticated)
    {
      this.isUserAdmin = this.authService.isUserAdmin();
    }
   }

  ngOnInit() {
    this.isEmpty=false;
    this.spin = true;
    this.getToys();
  }

  public getToys = () => {
    const apiAddress = 'api/toys';
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.toys = res as ToyModel[];
        if(this.toys.length==0)
        {
          this.isEmpty=true;
        this.spin = false;
        }
        this.spin = false;
      }),
      error => {
      };
  }

  public addToRecomendations=(id)=>{
    //const apiAddress = ;
    const recomendation: RecomendationDto = { 
      id: "00000000-0000-0000-0000-000000000000",
      goodId: id,
      goodType: "прикраса"
    };
    this.repository.create('api/SearchAndRecomendationResponse/addRecomendation',recomendation)
      .subscribe(_=>{
        this.router.navigate(['/home']);
      },
      error=>{
        this.errorMessage = error;
          this.showError = true;
      }   );
  }

  public deleteToy=(id: string)=>{
    var result = confirm("Впевнений, що хочеш видалити?");
    if (result) {
    const apiAddress = 'api/toys/delete?id='+id;
    this.repository.delete(apiAddress)
      .subscribe(()=>{
        this.spin = true;
        this.getToys();
      }),
      ()=>{
        this.showError = true;
        this.errorMessage = 'Неможливо видалити, спробуйте пізніше';
      };;
    }
  }

  public updateToy=(id: string)=>{
    const apiAddress ='/good/toy/update-toy/'+id;
    this.router.navigate([apiAddress]);
  }

  public addToy=()=>{
    this.router.navigate(['/good/toy/add-toy']);
  }

  public createImgPath = (serverPath: string) => {
    return this.repository.createImgPath(serverPath);
  }

}
