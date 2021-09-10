import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationExtras} from '@angular/router';
import { TreeForBasketModel } from '../../_interfaces/tree/treeForBasketModel';
import { OrderModule } from '../order.module';
import { RepositoryService } from '../../shared/services/repository.service';
import { FormBuilder, FormControl, FormGroup, Validators  } from '@angular/forms';
import { OrderModel } from 'src/app/_interfaces/order/orderModel';
import { DeliveryModel } from 'src/app/_interfaces/deliveryModel';
import { PaymentModel } from 'src/app/_interfaces/paymentModel';
import { ToyForBasketModel } from 'src/app/_interfaces/toy/toyForBasketModel';

@Component({
  selector: 'app-order-toy',
  templateUrl: './order-toy.component.html',
  styleUrls: ['./order-toy.component.css']
})
export class OrderToyComponent implements OnInit {

  deliveryTypes = ['Самовивіз з нової пошти', 'Самовивіз з Укрпошти ', "Доставка кур'єром нової пошти"];
  paymentTypes = ['Оплатити при отриманні доставки', "За реквізитами (власник зв'яжеться з вами та надасть дані)"]; 

  public dataAboutUserForm: FormGroup;
  public errorMessage = '';
  public showError: boolean;
  id: string;
  goodId: string;
  name: string;
  count: number;
  price: number;
  deliveryType: string;
  paymentType: string;
  order: OrderModule;
  deliveries: DeliveryModel[];
  payments: PaymentModel[];
  toys: ToyForBasketModel[];
  toy: ToyForBasketModel;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private repository: RepositoryService,
    private fb: FormBuilder
  )
 {
  //this.id = this.route.snapshot.paramMap.get('id')!;
  const navigation = this.router.getCurrentNavigation();
  const state = navigation.extras.state as {
    toyForBasketModel: ToyForBasketModel,
    treeForBasketModel: TreeForBasketModel,
    typeOfGood: string
  };
  if(state==null){
    this.router.navigate(['/toy']);
  }
  if(state.typeOfGood=="toy"){
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
  toys.push(this.toy);
  this.toys = toys;
  }
  
}

  ngOnInit() {
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
    this.deliveryType = delivery;
  }

  onChangePayment(payment) {
    this.paymentType = payment;
  }

  public validateControl = (controlName: string) => {
    return this.dataAboutUserForm.controls[controlName].invalid && this.dataAboutUserForm.controls[controlName].touched;
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.dataAboutUserForm.controls[controlName].hasError(errorName);
  }

  public createOrder = (formValue)=>{
    this.showError = false;
    const formValues = { ...formValue };
    const delivery : DeliveryModel = {
      id: "00000000-0000-0000-0000-000000000000",
      name:"hhhh",
      details:"jjjj",
      goodId: this.goodId
    }
    const payment : PaymentModel = {
      id: "00000000-0000-0000-0000-000000000000",
      status:"hhhhhh",
      goodId: this.goodId
    }
    var deliveries = Array();
    deliveries.push(delivery);
    var payments = Array();
    payments.push(payment);
    if(this.toys==undefined)
    {
      this.toys = [];      
    }
    const address: string = formValues.city + formValues.region;
  }
}
