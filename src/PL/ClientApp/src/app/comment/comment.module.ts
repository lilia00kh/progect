import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute, ParamMap  } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import {RxReactiveFormsModule } from "@rxweb/reactive-form-validators"
import { GetCommentsComponent } from './get-comments/get-comments.component';
import { AddCommentComponent } from './add-comment/add-comment.component';
//import { UpdateTreeComponent } from './update-tree/update-tree.component';
import { AdminGuard } from '../shared/guards/admin.guard';
import { AuthGuard } from '../shared/guards/auth.guard';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

@NgModule({
  declarations: [
    GetCommentsComponent,
    AddCommentComponent,
    //UpdateTreeComponent
],
  imports: [
    CommonModule,
    RxReactiveFormsModule,
    MatProgressSpinnerModule,
    FormsModule, 
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: '', component: GetCommentsComponent },
      { path: 'add-tree', component: AddCommentComponent, canActivate: [AuthGuard]  },
      //{ path: 'update-tree/:id', component: UpdateCommentComponent, canActivate: [AuthGuard, AdminGuard]  }
    ])
  ]
})
export class CommentModule { }
