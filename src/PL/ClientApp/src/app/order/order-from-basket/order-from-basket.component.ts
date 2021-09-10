import { TreeModel } from '../../_interfaces/tree/treeModel';
import { ToyModel } from '../../_interfaces/toy/toyModel';
import { BasketModel } from '../../_interfaces/basketModel';
import { PriceAndSizeModel } from '../../_interfaces/tree/priceAndSizeModel';
import { RepositoryService } from '../../shared/services/repository.service';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { Router, ActivatedRoute} from '@angular/router';
import { TreeForBasketModel } from "../../_interfaces/tree/treeForBasketModel";
import { ToyForBasketModel } from "../../_interfaces/toy/toyForBasketModel";
import { OrderModule } from '../order.module';
import { DeliveryModel } from 'src/app/_interfaces/deliveryModel';
import { PaymentModel } from 'src/app/_interfaces/paymentModel';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { OrderModel } from 'src/app/_interfaces/order/orderModel';

@Component({
  selector: 'app-order-from-basket',
  templateUrl: './order-from-basket.component.html',
  styleUrls: ['./order-from-basket.component.css']
})
export class OrderFromBasketComponent implements OnInit {
  

  deliveryTypes = ['самовивіз з Нової пошти', 'самовивіз з Укрпошти ', 'самовивіз з магазину'];
  paymentTypes = ['оплатити при отриманні доставки', "за реквізитами"]; 

  public dataAboutUserForm: FormGroup;
  public errorMessage = '';
  public showError: boolean;
  public paymentMessage = '';
  public showPaymentMessage: boolean;
  id: string;
  goodId: string;
  name: string;
  count: number;
  size: number;
  price: number;
  typeOfGood:string;
  deliveryType: string;
  paymentType: string;
  order: OrderModule;
  deliveries: DeliveryModel[];
  payments: PaymentModel[];
  trees: TreeForBasketModel[];
  tree: TreeForBasketModel;
  toys: ToyForBasketModel[];
  toy: ToyForBasketModel;
  isUserAuthenticated: boolean;
  isUserAdmin: boolean;
  goodForBaskeyId: string;
  color: string;
  showPayment: boolean;
  phoneError: boolean;
  showDetailsAboutDelivery: boolean;
  canCreateOrder: boolean;
  deliveryDitails: string;
  showSize: boolean;
  deliveryExist: boolean;
  paymentExist: boolean;
  showDetailsAboutNewPostDelivery: boolean;
  showColor: boolean;
  spin: boolean;
  navigation: { treeForBasketModel: TreeForBasketModel; toyForBasketModel: ToyForBasketModel; typeOfGood: string; goodForBaskeyId: string; };

  constructor(
    private route: ActivatedRoute,
    private authService: AuthenticationService,
    private router: Router,
    private repository: RepositoryService,
    private fb: FormBuilder
  )
 {
   
  if (this.router.getCurrentNavigation() != null) {
    const navigation = router.getCurrentNavigation().extras.state as {
      treeForBasketModel: TreeForBasketModel,
      toyForBasketModel: ToyForBasketModel,
      typeOfGood:string,
      goodForBaskeyId: string
    };
    console.log(navigation)
    this.spin = true;
    this.phoneError = false;
    this.isUserAuthenticated = this.authService.isUserAuthenticated();
    if(this.isUserAuthenticated)
    {
      this.isUserAdmin = this.authService.isUserAdmin();
    }
    
    const state = navigation;
    if(state==null){
      if(this.isUserAuthenticated){
        router.navigate(['/basket']);
      }
      else{
        router.navigate(['/']);
      }
    }
    this.goodForBaskeyId = state.goodForBaskeyId;
    this.typeOfGood = state.typeOfGood;  
    if(this.typeOfGood=="tree"){ 
      this.showColor = true;
      this.showSize = true;
    this.name = state.treeForBasketModel.name;
    this.count = state.treeForBasketModel.count;
    this.price = state.treeForBasketModel.price;
    this.color = state.treeForBasketModel.color;
    this.goodId = state.treeForBasketModel.treeId;
    this.size = state.treeForBasketModel.size;
      const tree: TreeForBasketModel = {
        id:"00000000-0000-0000-0000-000000000000",
        treeId:this.goodId,
        name:this.name,
        count:this.count,
        price:this.price,
        size:this.size,
        color:this.color
      }
      var trees= new Array();
      trees.push(tree);
      this.trees = trees;
    }  
    else{
      this.showColor = false;
      this.showSize = false;
    this.name = state.toyForBasketModel.name;
    this.count = state.toyForBasketModel.count;
    this.price = state.toyForBasketModel.price;
      this.goodId = state.toyForBasketModel.toyId;
      const toy: ToyForBasketModel = {
        id:"00000000-0000-0000-0000-000000000000",
        toyId:this.goodId,
        name:this.name,
        count:this.count,
        price:this.price
      }
      var toys= new Array();
      toys.push(toy);
      this.toys = toys; 
    }
    this.spin = false;
  }
  
}

  ngOnInit() {
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

  onChangeDelivery(delivery) {
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
      if(delivery=="самовивіз з нової пошти")
      this.showDetailsAboutNewPostDelivery = true;
      else
      this.showDetailsAboutDelivery = true;
      
    }
    this.deliveryType = delivery;
    this.deliveryExist = true;
  }

  onChangePayment(payment) {
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
      this.showError = true;
      this.errorMessage ="Некоректний ввід індекса";
      return;
    }
    this.showError = false;
    this.canCreateOrder = true;
    this.deliveryDitails = " індекс: " + index;
  }

  public saveDepartment =(department)=>{
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
      this.showError = true;
      this.errorMessage = "Будь ласка, оберіть тип доставки"
      return;
    }
    if(this.deliveryType!="самовивіз з магазину"){
      if(!this.paymentExist)
    {
      this.showError = true;
      this.errorMessage = "Будь ласка, оберіть спосіб оплати"
      return;
    }
    }
    
    if(!this.canCreateOrder)
    {
      this.showError = true;
      this.errorMessage = "Будь ласка, заповніть всі необхідні поля"
      return;
    }

    this.phoneError = false;
    this.showError = false;
    const formValues = { ...formValue };
    const delivery : DeliveryModel = {
      id: "00000000-0000-0000-0000-000000000000",
      name: this.deliveryType,
      details: this.deliveryDitails,
      goodId: this.goodId
    }
    const payment : PaymentModel = {
      id: "00000000-0000-0000-0000-000000000000",
      status: "не оплачено",
      goodId: this.goodId
    }
    var deliveries = Array();
    deliveries.push(delivery);
    var payments = Array();
    payments.push(payment);
    if(this.trees==undefined)
    {
      this.trees = [];      
    }
    if(this.toys==undefined)
    {
      this.toys = [];      
    }

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
      trees:this.trees,
      toys:this.toys,
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
      this.router.navigate(['/home']);
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
