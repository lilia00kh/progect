import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { ToyModel } from 'src/app/_interfaces/toy/toyModel';
import { RepositoryService } from '../../shared/services/repository.service';
import { FormBuilder } from '@angular/forms';
import { ImageModel } from 'src/app/_interfaces/imageModel';

@Component({
  selector: 'app-add-toy',
  templateUrl: './add-toy.component.html',
  styleUrls: ['./add-toy.component.css']
})
export class AddToyComponent implements OnInit {

  public addToyForm: FormGroup;
  public errorMessage = '';
  public showError: boolean;
  public images: ImageModel[] = [];

  
  constructor(
    private repository: RepositoryService,
    private _router: Router,
    private fb: FormBuilder)
      { }

  ngOnInit() : void {


    this.addToyForm = this.fb.group({
      name: [''],
      description: [''],
      price: ['']      
    });
    }

    public uploadFinished = (event) => {
      event.forEach(element => {
        this.images.push(element)
      });
    }

    public deleteImg=(imagePath)=>{
      var removeIndex = this.images.map(item => item.imagePath).indexOf(imagePath);
      ~removeIndex && this.images.splice(removeIndex, 1);
      var address = 'api/upload/DeleteImageByNameFromDB?imageName=' + imagePath;
      this.repository.delete(address).subscribe();
    }

  public addToy = (addToyFormValue) => {
    this.showError = false;
    this.errorMessage='';
    const formValues = { ...addToyFormValue };     
    if( this.images.length ==0)
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
      id: "00000000-0000-0000-0000-000000000000",
      name: formValues.name,
      description: formValues.description,
      price: formValues.price,
      imageModels: this.images
    };
    
    this.repository.create('api/toys/add', toy)
      .subscribe(_ => {
          this._router.navigate(['/good/toy']);
        },
        error => {
          this.errorMessage = error;
          this.showError = true;
        });
  }

}
