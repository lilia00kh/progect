import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { TreeModel } from 'src/app/_interfaces/tree/treeModel';
import { RepositoryService } from '../../shared/services/repository.service';
import { PriceAndSizeModel } from 'src/app/_interfaces/tree/priceAndSizeModel';
import { FormBuilder } from '@angular/forms';
import { FormArray } from '@angular/forms';
import { ImageModel } from 'src/app/_interfaces/imageModel';

@Component({
  selector: 'app-add-tree',
  templateUrl: './add-tree.component.html',
  styleUrls: ['./add-tree.component.css']
})
export class AddTreeComponent implements OnInit {
  public addTreeForm: FormGroup;
  public priceAndSizeModels: PriceAndSizeModel[];
  public priceAndSizeForm: FormGroup;
  public errorMessage = '';
  public showError: boolean;
  isCreate: boolean;
  public response: {dbPath: ''};
  imgPath: any;
  public images: ImageModel[] = [];
  treeTypes = ['литі', 'комбіновані','звичайні','штучні','засніжені']; 
  sizes = [0.20, 0.30, 0.40, 0.50, 0.60,0.70, 1.1, 1.2, 1.4,1.5, 1.6, 1.8, 2.1,2.2,2.4,2.5,2.7,3,3.5,4 ];
  treeType: string;
  treeColors = ['зелений', 'білий','голубий']; 
  treeColor: string;
  sizeForPrice: number;


  constructor(
    private repository: RepositoryService,
    private router: Router,
    private fb: FormBuilder)
      { }

  ngOnInit() : void {
    this.sizeForPrice=0.2;
    this.treeColor ='';
    this.treeType =''
    this.priceAndSizeModels= new Array();
    this.isCreate = false;

    this.addTreeForm = this.fb.group({
      name: [''],
      description: [''],
      price: [''],
      size:['']
    });
  }

  // onChange(s:number){
  //   this.sizeForPrice = s;
  // }

  onChangeTreeType(treeType) {
    this.treeType = treeType;
  }

  onChangeTreeColor(treeColor) {
    this.treeColor = treeColor;
  }

  public deletePriceAndSize(id:string){
    var result = confirm("Are you sure?");
if (result) {
  var removeIndex = this.priceAndSizeModels.map(item => item.id).indexOf(id);
  ~removeIndex && this.priceAndSizeModels.splice(removeIndex, 1);  
  const apiAddress = 'api/trees/DeletePriceAndSizeById?id='+id;
  this.repository.delete(apiAddress)
    .subscribe();
}
    
  }

  public deleteImg=(imagePath)=>{
    var removeIndex = this.images.map(item => item.imagePath).indexOf(imagePath);
    ~removeIndex && this.images.splice(removeIndex, 1);
    var address = 'api/upload/DeleteImageByNameFromDB?imageName=' + imagePath;
    this.repository.delete(address).subscribe();
  }

  public addTree = (addTreeFormValue) => {

    this.showError = false;
    this.errorMessage='';
    if( this.treeColor =='')
    {
      this.showError = true;
      this.errorMessage = 'Оберіть колір';
      return;
    }
    if( this.images.length ==0)
    {
      this.showError = true;
      this.errorMessage = 'Загрузіть фото';
      return;
    }
    if( this.treeType =='')
    {
      this.showError = true;
      this.errorMessage = 'Оберіть тип ялинки'
      return;
    }
    
    const formValues = { ...addTreeFormValue };
    console.log(formValues);
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

    var len = this.priceAndSizeModels.length;
    if(  len == 0)
    {
      this.showError = true;
      this.errorMessage = 'Введіть ціну і розмір'
      return;
    }
    var priceAndSizeModels= new Array();
    for(var i=0;i<len;i++)
    {
      var priceAndSizeModel: PriceAndSizeModel = {
        id: "00000000-0000-0000-0000-000000000000",
        price: this.priceAndSizeModels[i].price,
        size:  this.priceAndSizeModels[i].size
      }
      priceAndSizeModels.push(priceAndSizeModel);
    }

    
    const tree: TreeModel = {
      id: "00000000-0000-0000-0000-000000000000",
      name: formValues.name,
      description: formValues.description,
      priceAndSizeModels: priceAndSizeModels,
      imageModels: this.images,
      treeType: this.treeType,
      color: this.treeColor
    };
    
    this.repository.create('api/trees/add', tree)
      .subscribe(_ => {
          this.router.navigate(['/good/tree',this.treeType]);
        },
        error => {
          this.errorMessage = error;
          this.showError = true;
        });
  }

  get price() {
    return this.addTreeForm.get('price') as FormArray;
  }

  get size() {
    return this.addTreeForm.get('size') as FormArray;
  }

  public addPriceAndSize(addTreeFormValue){
    const formValues = { ...addTreeFormValue };
    if(formValues.size!=="")
    {
      this.sizeForPrice=parseFloat(formValues.size);
    }
    this.showError=false;
    this.errorMessage = '';
    // if((formValues.price=="" && this.sizeForPrice==0)||(formValues.price==null && this.sizeForPrice==0) )
    // {
    //   this.errorMessage = 'Введіть ціну і розмір';
    //   this.showError=true;
    //   return;
    // }
    if(formValues.price==""||(formValues.price==null))
    {
      this.errorMessage = 'Введіть ціну';
      this.showError=true;
      return;
    }
    if(formValues.price<=0)
    {
      this.errorMessage = 'Ціна повинна бути більша ніж 0';
      this.showError=true;
      return;
    }

    if(this.priceAndSizeModels.some(x=>x.size==this.sizeForPrice))
    {
      this.errorMessage = 'Такий розмір вже додано';
      this.showError=true;
      return;
    }

    var newPriceAndSizeModel:   PriceAndSizeModel =
    {
      id: "00000000-0000-0000-0000-000000000000",
    price: formValues.price,
    size:  this.sizeForPrice
  }
    this.priceAndSizeModels.push(newPriceAndSizeModel)
  }

  public uploadFinished = (event) => {
    event.forEach(element => {
      this.images.push(element)
    });
  }

  public returnToCreate = () => {
    this.isCreate = true;
  }

  public createImgPath = (serverPath: string) => {
    return this.repository.createImgPath(serverPath);
  }

}
