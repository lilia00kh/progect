import { TreeModel } from '../../_interfaces/tree/treeModel';
import { PriceAndSizeModel } from '../../_interfaces/tree/priceAndSizeModel';
import { RepositoryService } from '../../shared/services/repository.service';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { ActivatedRoute, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import {Event} from '@angular/router';
import { OrderModel } from 'src/app/_interfaces/order/orderModel';
import { OrderResponseModel } from 'src/app/_interfaces/order/orderResponseModel';

@Component({
  selector: 'app-get-orders',
  templateUrl: './get-orders.component.html',
  styleUrls: ['./get-orders.component.css']
})
export class GetOrdersComponent implements OnInit {
  public orders: OrderResponseModel[];
  public isUserAuthenticated: boolean;
  public isUserAdmin : boolean;
  spin:boolean;
  treeType: string;
  showError: boolean;
  errorMessage: string;
  constructor(private repository: RepositoryService,    
    private route: ActivatedRoute,
     private authService: AuthenticationService,
     private router: Router) {
    this.isUserAuthenticated = this.authService.isUserAuthenticated();
    if(this.isUserAuthenticated)
    {
      this.isUserAdmin = this.authService.isUserAdmin();
    }
   }

  ngOnInit() {
    this.spin =true;
    this.getOrders();
  }

  public getOrders = () => {
    const apiAddress = 'api/orders';
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.orders = res as OrderResponseModel[];
        if(this.orders.length==0)
        {

          this.showError = true;
        this.errorMessage = "Список замовлень порожній :("
        }
        this.spin = false;
      },
      error => {
        this.spin = false;
        this.showError = true;
        this.errorMessage = "Список замовлень порожній :("
      });
  }

  public changePaymentStatus = (order) => {
    if(order.paymentDetails==" статус оплати: оплачено"){
    alert("Не можливо змінити статус, оскільки вже проведена оплата");
    return;
    }
    var result = confirm("Впевнений, що хочеш змінити статус?");
    if (result) {
    const apiAddress = 'api/orders/changePaymentStatus';
    this.repository.update(apiAddress,order)
    .subscribe(res => {
      this.getOrders();
    });}
  }

  public deleteOrder = (id) => {
    var result = confirm("Впевнений, що хочеш видалити?");
    if (result) {
    const apiAddress = 'api/orders/deleteOrder?id='+id;
    this.repository.delete(apiAddress)
    .subscribe(res => {
      this.spin = true;
      this.getOrders();
    });
  }
  }

  public createImgPath = (serverPath: string) => {
    return this.repository.createImgPath(serverPath);
  }

}
