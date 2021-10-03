import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationExtras} from '@angular/router';
import { RepositoryService } from '../../shared/services/repository.service';
import { ToyModel } from '../../_interfaces/toy/toyModel';
import { ToyForBasketModel } from '../../_interfaces/toy/toyForBasketModel';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ImageModel } from 'src/app/_interfaces/imageModel';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { DeliveryModel } from 'src/app/_interfaces/deliveryModel';
import { PaymentModel } from 'src/app/_interfaces/paymentModel';
import { OrderModel } from 'src/app/_interfaces/order/orderModel';

@Component({
  selector: 'app-order-toy',
  templateUrl: './order-toy.component.html',
  styleUrls: ['./order-toy.component.css']
})
export class OrderToyComponent implements OnInit {

  deliveryTypes = ['самовивіз з Нової пошти', 'самовивіз з Укрпошти ', 'самовивіз з магазину'];
  paymentTypes = ['оплатити при отриманні доставки', 'за реквізитами']; 
  public showOrderForm: boolean;
  favoriteSeason: string;
  public toy: ToyModel;
  public price:number;
  public priceForForm:number;
  public count:number;
  public size: number;
  public sizes: number[];
  public treeForBasket: ToyForBasketModel;
  public name: string;
  public getToyByIdForm: FormGroup;
  public description : string;
  public isUserAuthenticated: boolean;
  public isUserAdmin : boolean;
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
  spin: boolean;
  paymentExist: boolean;
  deliveryExist: boolean;
  canCreateOrder: boolean;
  showDetailsAboutNewPostDelivery: boolean;
  phoneError: boolean;
  showDetailsAboutDelivery: boolean;
  showPayment: boolean;
  dataAboutUserForm: FormGroup;
  deliveryType: string;
  deliveryDitails: string;
  goodId: string;
  trees: any;
  toys: ToyForBasketModel[];
  goodForBaskeyId: any;
  showPaymentMessage: boolean;
  paymentMessage: string;
  paymentType: any;
  countForOrder: number;
  showFormError: boolean;

   isNumeric(value):boolean {
    return /^-?\d+$/.test(value);
}

  onKey(event: any) {
    this.showAlert = false;
    this.displayPriceForForm = false;    
    this.priceForForm = 1;
  if(event.target.value=="")
  {
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
  this.displayPriceForForm = true;
    this.showError = false;
    this.errorMessage = ""
    this.priceForForm = this.price * this.count;
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthenticationService,
    private repository: RepositoryService,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.spin = true;
    this.id = this.route.snapshot.paramMap.get('id')!;
    this.getToy(this.id);
    this.getToyByIdForm = new FormGroup(
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

  public getToy = (id:string) => {
    const apiAddress = 'api/toys/getbyid?id='+id;
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.toy = res as ToyModel;
        this.name = this.toy.name as string;
        this.description = this.editDescription(this.toy.description as string);
        this.price = this.toy.price as number;
        this.images = this.toy.imageModels as ImageModel[];
        this.img = this.createImgPath(this.images[0].imagePath);
        this.i =0;
        this.priceForForm = 1;
        this.spin = false;
      },
      ()=>{
        this.router.navigate(['/not-found']);
      });
  }

  public addToyToBasket = (name:string, getToyByIdFormValue)=>{
    this.isUserAuthenticated = this.authService.isUserAuthenticated();
    if(!this.isUserAuthenticated)
    {
      this.showAlert= true;
      this.alertMessage = "Будь ласка, ввійдіть у систему";
      return;
    }
    if(this.priceForForm == 1)
    {
      this.showAlert= true;
      this.alertMessage = "Будь ласка, введіть кількість";
      return;
    }
    this.showError = false;
    const formValues = { ...getToyByIdFormValue };
    const toyForBasketModel: ToyForBasketModel = {
      id: "00000000-0000-0000-0000-000000000000",
      toyId: this.id,
      name: name,
      price: this.priceForForm,
      count: formValues.count      
    };
    this.repository.create('api/baskets/addToyToBasket', toyForBasketModel)
    .subscribe(_ => {
        this.router.navigate(['/good/toy']);
      },
      error => {
        this.errorMessage = error;
        this.showError = true;
      });
  }

  public orderToy=(getToyByIdFormValue)=>{
    if(this.priceForForm == 1)
    {
      this.showAlert= true;
      this.alertMessage = "Будь ласка, введіть кількість";
      return;
    }
    this.showError = false;
    this.showOrderForm=true;
     const formValues = { ...getToyByIdFormValue };
     this.countForOrder = formValues.count;
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
    this.showFormError = false;
    this.canCreateOrder = false;
    var reg = /^\d+$/;
    if(!reg.test(index))
    {
      this.showError = true;
      this.errorMessage ="Некоректний ввід індекса";
      return;
    }
    this.showError = false;
    this.canCreateOrder = true;
    this.deliveryDitails = " індекс: " + index;
  }

  public saveDepartment =(department)=>{
    this.showFormError = false;
    this.canCreateOrder = false;
    var reg = /^\d+$/;
    if(!reg.test(department))
    {
      this.showError = true;
      this.errorMessage ="Некоректний ввід відділення";
      return;
    }
    this.showError = false;
    this.canCreateOrder = true;
    this.deliveryDitails = " відділення № " + department;
  }

  public createOrder = (formValue)=>{
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
    this.trees = [];      

   const toyForBasketModel: ToyForBasketModel = {
      id: "00000000-0000-0000-0000-000000000000",
      toyId: this.id,
      name: this.name,
      price:  this.priceForForm,
      count: this.countForOrder,
    };
    var toys = new Array();
    toys.push(toyForBasketModel);
    if(!this.phoneValidation(formValues.phone))
    {
      this.phoneError = true;
      return;
    }
    this.spin=true;
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
      trees:this.trees,
      toys: toys,
      deliveries: deliveries,
      payments: payments,
      date: currentDate
    };

    this.repository.create('api/orders/add', order)
    .subscribe(_ => {
      
      if(this.goodForBaskeyId!=null)
      {
        this.deleteGood(this.goodForBaskeyId);
      }
      this.router.navigate(['/success-order']);
      },
      err => {
        this.spin=false;
        this.errorMessage = err;
        this.showError = true;
      });
    }
    public deleteGood=(id:string)=>{
      const apiAddress = 'api/baskets/deleteGoodFromBasket?detailsId='+id;
      this.repository.delete(apiAddress)
        .subscribe(     
        ); 
    }

  public createImgPath = (serverPath: string) => {
    return this.repository.createImgPath(serverPath);
  }

}
