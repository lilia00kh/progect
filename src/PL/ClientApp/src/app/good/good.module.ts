import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule  } from '@angular/router';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import {RxReactiveFormsModule } from "@rxweb/reactive-form-validators"
import { GetToysComponent } from './get-toys/get-toys.component';
import { AddToyComponent } from './add-toy/add-toy.component';
import { UpdateToyComponent } from './update-toy/update-toy.component';
import { OrderToyComponent } from './order-toy/order-toy.component';
import { AdminGuard } from '../shared/guards/admin.guard';
import { AuthGuard } from '../shared/guards/auth.guard';
import { GetToyByIdComponent } from './get-toy-by-id/get-toy-by-id.component';
import { GetTreeComponent } from './get-trees/get-trees.component';
import { OrderTreeComponent } from './order-tree/order-tree.component';
import { AddTreeComponent } from './add-tree/add-tree.component';
import { UpdateTreeComponent } from './update-tree/update-tree.component';
import { UploadComponent } from '../upload/upload.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { NgxSliderModule } from '@angular-slider/ngx-slider';


@NgModule({
  declarations: [
    GetToysComponent,
    AddToyComponent,
    GetToyByIdComponent,
    UpdateToyComponent,
    UploadComponent,
    OrderToyComponent,
    GetTreeComponent,
    AddTreeComponent,
    UpdateTreeComponent,
    OrderTreeComponent
],
  imports: [
    CommonModule,
    NgxSliderModule,
    FormsModule,
    RxReactiveFormsModule,
    FormsModule, 
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    RouterModule.forChild([
      { path: 'toy', component: GetToysComponent },
      { path: 'toy/add-toy', component: AddToyComponent, canActivate: [AuthGuard, AdminGuard]  },
      { path: 'toy/:id', component: GetToyByIdComponent },
      { path: 'toy/update-toy/:id', component: UpdateToyComponent, canActivate: [AuthGuard, AdminGuard]  },
      { path: 'toy/order-toy/:id', component: OrderToyComponent  },
      { path: 'tree/:treeType', component: GetTreeComponent },
      { path: 'tree/add-tree/new', component: AddTreeComponent, canActivate: [AuthGuard, AdminGuard]},      
      { path: 'tree/update-tree/:id', component: UpdateTreeComponent, canActivate: [AuthGuard, AdminGuard]  },
      { path: 'tree/order-tree/:id', component: OrderTreeComponent }
    ])
  ]
})
export class GoodModule { }
