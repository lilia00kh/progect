import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationExtras} from '@angular/router';
import { RepositoryService } from '../../shared/services/repository.service';
import { TreeModel } from '../../_interfaces/tree/treeModel';
import { TreeForBasketModel } from '../../_interfaces/tree/treeForBasketModel';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PriceAndSizeModel } from '../../_interfaces/tree/priceAndSizeModel';
import { ImageModel } from 'src/app/_interfaces/imageModel';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { DeliveryModel } from 'src/app/_interfaces/deliveryModel';
import { PaymentModel } from 'src/app/_interfaces/paymentModel';
import { OrderModel } from 'src/app/_interfaces/order/orderModel';
import { ToyForBasketModel } from 'src/app/_interfaces/toy/toyForBasketModel';

@Component({
  selector: 'app-order-tree',
  templateUrl: './order-tree.component.html',
  styleUrls: ['./order-tree.component.css']
})
export class OrderTreeComponent implements OnInit {
  deliveryTypes = ['самовивіз з Нової пошти', 'самовивіз з Укрпошти ', 'самовивіз з магазину'];
  paymentTypes = ['оплатити при отриманні доставки', 'за реквізитами']; 
  favoriteSeason: string;
  public tree: TreeModel;
  public price:number;
  public priceForForm:number;
  public count:number;
  public size: number;
  public color: string;
  public sizes: number[];
  public treeForBasket: TreeForBasketModel;
  public name: string;
  public getTreeByIdForm: FormGroup;
  public description : string;
  public isUserAuthenticated: boolean;
  public isUserAdmin : boolean;
  public priceAndSizeModels: PriceAndSizeModel[];
  public errorMessage = '';
  public showError: boolean;
  public id : string;
  public selectedSize: number;
  images: ImageModel[];
  public img: string;
  public i: number;
  public displayPriceForForm: boolean; 
  showAlert: boolean;
  public alertMessage:string;
  public nav: NavigationExtras;

  spin: boolean;
  countForOrder: any;
  showOrderForm: boolean;
  paymentExist: boolean;
  deliveryExist: boolean;
  canCreateOrder: boolean;
  showDetailsAboutDelivery: boolean;
  showDetailsAboutNewPostDelivery: boolean;
  phoneError: boolean;
  showPayment: boolean;
  dataAboutUserForm: FormGroup;
  deliveryDitails: string;
  showPaymentMessage: boolean;
  deliveryType: any;
  paymentMessage: string;
  paymentType: any;
  toys: ToyForBasketModel[];
  goodForBaskeyId: any;
  showFormError: boolean;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthenticationService,
    private router: Router,
    private repository: RepositoryService,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.spin = true;
    this.id = this.route.snapshot.paramMap.get('id')!;
    this.getTree(this.id);
    this.getTreeByIdForm = new FormGroup(
      {
        count: new FormControl()
      }
    );
    this.showOrderForm=false;
    this.paymentExist = false;
    this.deliveryExist = false;
    this.canCreateOrder = false;
    this.showDetailsAboutDelivery = false;
    this.showDetailsAboutNewPostDelivery = false;
    this.phoneError = false;
    this.showPayment = true;
    this.dataAboutUserForm = new FormGroup({
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      email: new FormControl('', [Validators.required, Validators.email]),
      phone: new FormControl('', [Validators.required]),
      city: new FormControl('', [Validators.required]),
      region: new FormControl('', [Validators.required])
    });
  }

  onChange(priceAndSizeModel) {
    this.showAlert = false;
  this.displayPriceForForm = false;    
    this.priceForForm = 1;
    if(isNaN(this.count)||this.count==0)
  {
    this.size = priceAndSizeModel.size;
    this.price = priceAndSizeModel.price;
    this.displayPriceForForm = false;
    return;
  }
  this.displayPriceForForm =true;
    this.showError = false;
    this.errorMessage = "";
    this.size = priceAndSizeModel.size;
    this.price = priceAndSizeModel.price;
    this.priceForForm = this.price * this.count;
    
  }

   isNumeric(value):boolean {
    return /^-?\d+$/.test(value);
  }

  onKey(event: any) {
  
    this.showAlert = false;
    this.displayPriceForForm = false;    
    this.priceForForm = 1;
  if(event.target.value=="")
  {
    this.count =  event.target.value;
    this.showError = false;
    this.errorMessage = "";
    return;
  }
  this.count =  event.target.value;
  if(!this.isNumeric(this.count)){
    this.showError = true;
    this.errorMessage = "Кількість повинна бути цілим числом";
    return;
  }
  if(this.count <= 0 ){
    this.showError = true;
    this.errorMessage = "Кількість повинна бути більшою за 0";
    return;
  }
  if(isNaN(this.size) )
    {
      this.showError = false;
    this.errorMessage = "";
    return;
    }
  this.displayPriceForForm = true;
    this.showError = false;
    this.errorMessage = ""
    this.priceForForm = this.price * this.count;
  }


  public nextImg =()=>{
    this.i++;
    if(this.i<this.images.length)
    this.img = this.images[this.i].imagePath;
    else{
      this.i=0;
      this.img = this.images[this.i].imagePath;
    }
  }

  public priviosImg =()=>{
    this.i--;
    if(this.i<0)
    {
      this.i=this.images.length-1;
      this.img = this.images[this.i].imagePath;}
    else
    this.img = this.images[this.i].imagePath;
  }

  public editDescription =(description)=>{
    return description.split('\n');
  }

  public getTree = (id:string) => {
    const apiAddress = 'api/trees/getbyid?id='+id;
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.tree = res as TreeModel;
        this.name = this.tree.name as string;
        this.description = this.editDescription(this.tree.description as string);
        this.priceAndSizeModels = this.tree.priceAndSizeModels as PriceAndSizeModel[];
        this.color = this.tree.color;
        this.images = this.tree.imageModels as ImageModel[];
        this.img = this.createImgPath(this.images[0].imagePath);
        this.i =0;
        this.spin = false;
      },
      ()=>{
        this.router.navigate(['/not-found']);
      });
  }

  public addTreeToBasket = (name:string, getTreeByIdFormValue)=>{
    this.isUserAuthenticated = this.authService.isUserAuthenticated();
    if(!this.isUserAuthenticated)
    {
      this.showAlert= true;
      this.alertMessage = "Будь ласка, ввійдіть у систему";
      return;
    }
    if(isNaN(this.size))
    {
    this.showAlert= true;
    this.alertMessage = "Будь ласка, вкажіть розмір";
    return;
    }
    if(this.priceForForm == 1)
    {
      this.showAlert= true;
      this.alertMessage = "Будь ласка, введіть кількість";
      return;
    }
    this.showError = false;
    const formValues = { ...getTreeByIdFormValue };
    const treeForBasketModel: TreeForBasketModel = {
      id: "00000000-0000-0000-0000-000000000000",
      treeId: this.id,
      name: name,
      price: this.priceForForm,
      count: formValues.count,
      size: this.size,
      color: this.color
      
    };
    this.repository.create('api/baskets/addTreeToBasket', treeForBasketModel)
    .subscribe(_ => {
        this.router.navigate(['/basket']);
      },
      error => {
        this.errorMessage = error;
        this.showError = true;
      });
  }

  public orderTree = ( getTreeByIdFormValue)=>{
    if(isNaN(this.size))
    {
    this.showAlert= true;
    this.alertMessage = "Будь ласка, вкажіть розмір";
    return;
    }
    if(this.priceForForm == 1)
    {
      this.showAlert= true;
      this.alertMessage = "Будь ласка, введіть кількість";
      return;
    }
    this.showError = false;
    const formValues = { ...getTreeByIdFormValue };
    this.countForOrder = formValues.count;
    this.showOrderForm = true;
    // const treeForBasketModel: TreeForBasketModel = {
    //   id: "00000000-0000-0000-0000-000000000000",
    //   treeId: this.id,
    //   name: this.name,
    //   price: this.priceForForm,
    //   count: formValues.count,
    //   size: this.size,
    //   color: this.color  
    // };
     
    // const navigationExtrasForTree: NavigationExtras = {
    //   state: {
    //     treeForBasketModel: treeForBasketModel,
    //     toyForBasketModel: null,
    //     typeOfGood: "tree"
    //   }
    // };

    // this.nav = navigationExtrasForTree;
    
    // console.log(this.nav);
    // this.router.navigate(['/order/order-from-basket'], this.nav);
this.showOrderForm = true;
  }

  public createImgPath = (serverPath: string) => {
    return this.repository.createImgPath(serverPath);
  }

  onChangeDelivery(delivery) {
    this.showFormError = false;
    this.deliveryExist = false;
    this.showDetailsAboutDelivery = false;
    this.showDetailsAboutNewPostDelivery = false;
    if(delivery=="самовивіз з магазину")
    {
      this.deliveryDitails = "";
      this.canCreateOrder = true;
      this.showPayment =false;
      this.showPaymentMessage = false;
    }
    else
    {
      this.canCreateOrder = false;
      this.showPayment = true;
      if(delivery=="самовивіз з Нової пошти")
      this.showDetailsAboutNewPostDelivery = true;
      else
      this.showDetailsAboutDelivery = true;
      
    }
    this.deliveryType = delivery;
    this.deliveryExist = true;
  }

  onChangePayment(payment) {
    this.showFormError = false;
    this.paymentExist = false;
    this.showPaymentMessage = true;
    if(payment=="оплатити при отриманні доставки")
    {
    this.paymentMessage = "Необхідна мінімальна передоплата (з Вами зв'яжеться менеджер)."}
    else{
      this.paymentMessage = "З Вами зв'яжеться менеджер  та надасть дані.";
    }
    this.paymentType = payment;
    this.paymentExist = true;
  }

  public validateControl = (controlName: string) => {
    return this.dataAboutUserForm.controls[controlName].invalid && this.dataAboutUserForm.controls[controlName].touched;
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.dataAboutUserForm.controls[controlName].hasError(errorName);
  }

  public phoneValidation=(phone:string)=>{
    let re =  /^\+380\d{2}\d{2}\d{2}\d{3}$/;
    return re.test(phone);
  }

  public saveIndex =(index)=>{
    this.canCreateOrder = false;
    var reg = /^\d+$/;
    if(!reg.test(index))
    {
      this.showFormError = true;
      this.errorMessage ="Некоректний ввід індекса";
      return;
    }
    this.showFormError = false;
    this.canCreateOrder = true;
    this.deliveryDitails = " індекс: " + index;
  }

  public saveDepartment =(department)=>{
    this.canCreateOrder = false;
    var reg = /^\d+$/;
    if(!reg.test(department))
    {
      this.showFormError = true;
      this.errorMessage ="Некоректний ввід відділення";
      return;
    }
    this.showFormError = false;
    this.canCreateOrder = true;
    this.deliveryDitails = " відділення № " + department;
  }

  public createOrder = (formValue)=>{
    this.showError=false;
    if(!this.deliveryExist)
    {
      this.showFormError = true;
      this.errorMessage = "Будь ласка, оберіть тип доставки"
      return;
    }
    if(this.deliveryType!="самовивіз з магазину"){
      if(!this.paymentExist)
    {
      this.showFormError = true;
      this.errorMessage = "Будь ласка, оберіть спосіб оплати"
      return;
    }
    }
    
    if(!this.canCreateOrder)
    {
      this.showFormError = true;
      this.errorMessage = "Будь ласка, заповніть всі необхідні поля"
      return;
    }

    this.phoneError = false;
    this.showFormError = false;
    const formValues = { ...formValue };
    const delivery : DeliveryModel = {
      id: "00000000-0000-0000-0000-000000000000",
      name: this.deliveryType,
      details: this.deliveryDitails,
      goodId: this.id
    }
    const payment : PaymentModel = {
      id: "00000000-0000-0000-0000-000000000000",
      status: "не оплачено",
      goodId: this.id
    }
    var deliveries = Array();
    deliveries.push(delivery);
    var payments = Array();
    payments.push(payment);
    // if(this.trees==undefined)
    // {
      this.toys = [];      
    // }
    // if(this.toys==undefined)
    // {
    //   this.toys = [];      
    // }

    const tree: TreeForBasketModel = {
      id:"00000000-0000-0000-0000-000000000000",
      treeId:this.id,
      name:this.name,
      count:this.countForOrder,
      price:this.price,
      size:this.size,
      color:this.color
    }
    var trees = new Array();
    trees.push(tree);
    if(!this.phoneValidation(formValues.phone))
    {
      this.phoneError = true;
      return;
    }
    const address: string = " населений пункт: "+ formValues.city+", область: " + formValues.region;
    var currentDate = new Date();
    currentDate.toJSON();  
    const order: OrderModel = {
      user:"",
      firstName: formValues.firstName,
      lastName:  formValues.lastName,
      userEmail: formValues.email,
      address: address,
      phone: formValues.phone,
      trees:trees,
      toys: this.toys,
      deliveries: deliveries,
      payments: payments,
      date: currentDate
    };

    this.repository.create('api/orders/add', order)
    .subscribe(_ => {     
    this.spin=true;
      if(this.goodForBaskeyId!=null)
      {
        this.deleteGood(this.goodForBaskeyId);
      }
      this.router.navigate(['/success-order']);
      },
      error => {
        this.errorMessage = error;
        this.showError = true;
      });
    }
    public deleteGood=(id:string)=>{
      const apiAddress = 'api/baskets/deleteGoodFromBasket?detailsId='+id;
      this.repository.delete(apiAddress)
        .subscribe(     
        ); 
    }


}
