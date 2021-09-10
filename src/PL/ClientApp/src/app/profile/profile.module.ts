import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditComponent } from './edit/edit.component';
import { RouterModule } from '@angular/router';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import {RxReactiveFormsModule } from "@rxweb/reactive-form-validators";
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

@NgModule({
  declarations: [EditComponent],
  imports: [
    CommonModule,
    RxReactiveFormsModule,
    MatProgressSpinnerModule,
    FormsModule, 
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'edit', component: EditComponent }
    ])
  ]
})
export class ProfileModule { }
