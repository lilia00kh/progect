import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute} from '@angular/router';
import { RepositoryService } from '../../shared/services/repository.service';
import { ToyModel } from '../../_interfaces/toy/toyModel';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ImageModel } from 'src/app/_interfaces/imageModel';
import { AuthenticationService } from '../../shared/services/authentication.service';


@Component({
  selector: 'app-update-toy',
  templateUrl: './update-toy.component.html',
  styleUrls: ['./update-toy.component.css']
})
export class UpdateToyComponent implements OnInit {

  public updateToyForm: FormGroup;
  public toy: ToyModel;
  public errorMessage = '';
  public showError: boolean;
  public id : string;
  public images: ImageModel[] = [];
  public imagesFromDB: ImageModel[] = [];
  public imagesThatHaveToDeleteInDB: ImageModel[] = [];


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private repository: RepositoryService,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id')!;
    this.getToy(this.id);
    this.updateToyForm = this.fb.group({
      name: [''],
      description: [''],
      price: ['']
    });
  }

  public getToy = (id:string) => {
    const apiAddress = 'api/toys/getbyid?id='+id;
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.toy = res as ToyModel;
        this.imagesFromDB = this.toy.imageModels as ImageModel[];
        this.imagesFromDB.forEach(element => {
          this.images.push(element);
        });
        this.resetForm();
      },
      ()=>{
        this.router.navigate(['/not-found']);
      });
  }

  resetForm() {
    this.updateToyForm.controls["name"].setValue(this.toy.name);
    this.updateToyForm.controls["description"].setValue(this.toy.description);
    this.updateToyForm.controls["price"].setValue(this.toy.price);    
  }

  public uploadFinished = (event) => {
    event.forEach(element => {
      this.images.push(element)
    });
  }

  public updateToy = (updateToyFormValue) => {
    this.showError = false;
    this.errorMessage='';
    const formValues = { ...updateToyFormValue };     
    if( this.images.length ==0 && this.imagesFromDB.length==0)
    {
      this.showError = true;
      this.errorMessage = 'Загрузіть фото';
      return;
    }
    if( formValues.name =='')
    {
      this.showError = true;
      this.errorMessage = 'Введіть назву'
      return;
    }

    if( formValues.description =='')
    {
      this.showError = true;
      this.errorMessage = 'Введіть опис'
      return;
    }
    if( formValues.price ==0)
    {
      this.showError = true;
      this.errorMessage = 'Введіть ціну';
      return;
    }

    if( formValues.price <=0)
    {
      this.showError = true;
      this.errorMessage = 'Ціна повинна бути більшою за 0';
      return;
    }
    const toy: ToyModel = {
      id: this.id,
      name: formValues.name,
      description: formValues.description,
      price: formValues.price,
      imageModels: this.images
    };
    this.repository.update('api/toys/update', toy)
      .subscribe(_ => {
        this.imagesThatHaveToDeleteInDB.forEach(element=>{
          var address = 'api/upload/DeleteImageByNameFromDB?imageName=' + element.imageName;
          this.repository.delete(address).subscribe();
        });
          this.router.navigate(['/good/toy']);
        },
        error => {
          this.errorMessage = error;
          this.showError = true;
        });
  }

  public deleteImg=(imagePath)=>{
    var removeIndex = this.images.map(item => item.imagePath).indexOf(imagePath);
    this.imagesThatHaveToDeleteInDB.push(this.images[removeIndex]);
    ~removeIndex && this.images.splice(removeIndex, 1);
    removeIndex = this.imagesFromDB.map(item => item.imagePath).indexOf(imagePath);
    ~removeIndex && this.imagesFromDB.splice(removeIndex, 1);
  }

  public createImgPath = (serverPath: string) => {
    return this.repository.createImgPath(serverPath);
  }
  
}
