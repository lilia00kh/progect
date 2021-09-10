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
  selector: 'app-order-tree',
  templateUrl: './order-tree.component.html',
  styleUrls: ['./order-tree.component.css']
})
export class OrderTreeComponent implements OnInit {

  deliveryTypes = ['Самовивіз з нової пошти', 'Самовивіз з Укрпошти ', "Доставка кур'єром нової пошти"];
  paymentTypes = ['Оплатити при отриманні доставки', "За реквізитами (власник зв'яжеться з вами та надасть дані)"]; 

  public dataAboutUserForm: FormGroup;
  public errorMessage = '';
  public showError: boolean;
  id: string;
  goodId: string;
  name: string;
  count: number;
  size: number;
  price: number;
  deliveryType: string;
  paymentType: string;
  order: OrderModule;
  deliveries: DeliveryModel[];
  payments: PaymentModel[];
  trees: TreeForBasketModel[];
  tree: TreeForBasketModel;
  color: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private repository: RepositoryService
  )
 {
  //this.id = this.route.snapshot.paramMap.get('id')!;
  const navigation = this.router.getCurrentNavigation();
  const state = navigation.extras.state as {
    treeForBasketModel: TreeForBasketModel
    toyForBasketModel: ToyForBasketModel,
    typeOfGood: string
  };
  if(state==null){
    this.router.navigate(['/tree']);
  }
  if(state.typeOfGood=="tree"){
    this.name = state.treeForBasketModel.name;
  this.count = state.treeForBasketModel.count;
  this.price = state.treeForBasketModel.price;
  this.goodId = state.treeForBasketModel.treeId;
  this.size = state.treeForBasketModel.size;
  this.color = state.treeForBasketModel.color;
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
}

  ngOnInit() {
    //this.id = this.route.snapshot.paramMap.get('id')!;
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
    if(this.trees==undefined)
    {
      this.trees = [];      
    }
    const address: string = formValues.city + formValues.region;
  }
}
