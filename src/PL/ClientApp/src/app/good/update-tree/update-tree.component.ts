import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute} from '@angular/router';
import { RepositoryService } from '../../shared/services/repository.service';
import { TreeModel } from '../../_interfaces/tree/treeModel';
import { FormBuilder } from '@angular/forms';
import { FormGroup} from '@angular/forms';
import { PriceAndSizeModel } from '../../_interfaces/tree/priceAndSizeModel';
import { ImageModel } from 'src/app/_interfaces/imageModel';


@Component({
  selector: 'app-update-tree',
  templateUrl: './update-tree.component.html',
  styleUrls: ['./update-tree.component.css']
})
export class UpdateTreeComponent implements OnInit {
  public updateTreeForm: FormGroup;
  public tree: TreeModel;
  public priceAndSizeModels: PriceAndSizeModel[];
  public errorMessage = '';
  public showError: boolean;
  public id : string;
  public images: ImageModel[] = [];
  public imagesFromDB: ImageModel[] = [];
  public imagesThatHaveToDeleteInDB: ImageModel[] = [];
  treeType: string;
  treeTypes = ['литі', 'комбіновані','звичайні','штучні','засніжені']; 
  sizes = [0.20, 0.30, 0.40, 0.50, 0.60,0.70, 1.1, 1.2, 1.4,1.5, 1.6, 1.8, 2.1,2.2,2.4,2.5,2.7,3,3.5,4 ];
  treeColors = ['зелений', 'білий','голубий']; 
  treeColor: string;
  sizeForPrice: number;
  

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private repository: RepositoryService,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.sizeForPrice=0.2;
    this.treeType ='';
    this.treeColor ='';
    this.id = this.route.snapshot.paramMap.get('id')!;
    this.getTree(this.id);
    this.updateTreeForm = this.fb.group({
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

  public getTree = (id:string) => {
    const apiAddress = 'api/trees/getbyid?id='+id;
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.tree = res as TreeModel;
        this.priceAndSizeModels = this.tree.priceAndSizeModels as PriceAndSizeModel[];
        this.imagesFromDB = this.tree.imageModels as ImageModel[];
        this.imagesFromDB.forEach(element => {
          this.images.push(element);
        });
        this.resetForm();
      },
      ()=>{
        this.router.navigate(['/not-found']);
      });
  }

  public deletePriceAndSize(id:string){
    var result = confirm("Впевнений, що хочеш видалити?");
    if (result) {
  var removeIndex = this.priceAndSizeModels.map(item => item.id).indexOf(id);
  ~removeIndex && this.priceAndSizeModels.splice(removeIndex, 1);  
  const apiAddress = 'api/trees/DeletePriceAndSizeById?id='+id;
  this.repository.delete(apiAddress)
    .subscribe();
}
    
  }

  public addPriceAndSize(addTreeFormValue){
    const formValues = { ...addTreeFormValue };
   
    if(formValues.size!=="")
    {
      this.sizeForPrice=parseFloat(formValues.size);
    }
    this.showError=false;
    this.errorMessage = '';
    // if((formValues.price=="")||(formValues.price==null ) )
    // {
    //   this.errorMessage = 'Введіть ціну';
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

  resetForm() {
    this.updateTreeForm.controls["name"].setValue(this.tree.name);
    this.updateTreeForm.controls["description"].setValue(this.tree.description);
  }

  public uploadFinished = (event) => {
    event.forEach(element => {
      this.images.push(element)
    });
  }

  public updateTree = (updateTreeFormValue) => {
    this.showError = false;
    this.errorMessage='';
    if( this.treeColor =='')
    {
      this.showError = true;
      this.errorMessage = 'Оберіть колір';
      return;
    }
    if( this.images.length ==0 && this.imagesFromDB.length==0)
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
    
    const formValues = { ...updateTreeFormValue };
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
    len = formValues.price.length;
    for(var i=0;i<len;i++)
    {
      var priceAndSizeModel: PriceAndSizeModel = {
        id: this.priceAndSizeModels[i].id,
        price: this.priceAndSizeModels[i].price,
        size:  this.priceAndSizeModels[i].size
      }
      priceAndSizeModels.push(priceAndSizeModel);
    }
    const tree: TreeModel = {
      id: this.id,
      name: formValues.name,
      description: formValues.description,
      priceAndSizeModels: this.priceAndSizeModels,
      imageModels: this.images,
      treeType: this.treeType,
      color: this.treeColor
    };
    this.repository.update('api/trees/update', tree)
      .subscribe(_ => {
        console.log( this.imagesThatHaveToDeleteInDB);
        this.imagesThatHaveToDeleteInDB.forEach(element=>{
          var address = 'api/upload/DeleteImageByNameFromDB?imageName=' + element.imageName;
          this.repository.delete(address).subscribe();
        });
          this.router.navigate(['/good/tree',this.treeType]);
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
