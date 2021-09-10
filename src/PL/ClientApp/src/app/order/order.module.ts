import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import {RxReactiveFormsModule } from "@rxweb/reactive-form-validators"
import { AdminGuard } from '../shared/guards/admin.guard';
import { AuthGuard } from '../shared/guards/auth.guard';
import { OrderFromBasketComponent } from './order-from-basket/order-from-basket.component';
import { OrderToyComponent } from './order-toy/order-toy.component';
import { OrderTreeComponent } from './order-tree/order-tree.component';
import { GetOrdersComponent } from './get-orders/get-orders.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

@NgModule({
  declarations: [
      OrderFromBasketComponent,
      OrderToyComponent,
      OrderTreeComponent,
      GetOrdersComponent
],
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    RxReactiveFormsModule,
    FormsModule, 
    ReactiveFormsModule,
    RouterModule.forChild([
      // { path: 'order-from-basket', component: OrderFromBasketComponent},
      // { path: 'order-tree', component: OrderToyComponent },
      // { path: 'order-toy', component: OrderTreeComponent},
      { path: 'get-orders', component: GetOrdersComponent, canActivate: [AuthGuard]}
    ])
  ]
})
export class OrderModule { }
