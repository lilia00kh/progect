import { RepositoryService } from './../../shared/services/repository.service';
import { Component, OnInit  } from '@angular/core';
import { FormBuilder, FormsModule, FormGroup } from '@angular/forms';
import { ProfileModel } from './../../_interfaces/profileModel';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit  {
    public profileForm: FormGroup;
    public profileModel: ProfileModel = {
      firstName: '',
      lastName:  '',
      email: ''
    }
    public editProfileModel: ProfileModel = {
      firstName: '',
      lastName:  '',
      email: ''
    }

  constructor(private fb: FormBuilder, private repository: RepositoryService ){
    
  }

  ngOnInit(){
    this.getProfile();

    // Create FormGroup
    this.profileForm = this.fb.group({
     firstName:  this.profileModel.firstName,
     lastName: this.profileModel.lastName
    });

    // Set Values
    this.profileForm.controls["firstName"].setValue(this.profileForm.value.firstName);
    this.profileForm.controls["lastName"].setValue(this.profileForm.value.lastName);
  }

  public getProfile = () => {
    const apiAddress = 'api/profile';
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.profileModel = res as ProfileModel;
        this.editProfileModel=this.profileModel;
        this.resetForm();
      });
  }
  
  public updateProfile = () => {
    const apiAddress = 'api/profile/update';
    this.repository.update(apiAddress,this.profileModel)
      .subscribe();
  }

  resetForm() {
    this.profileForm.controls["firstName"].setValue(this.editProfileModel.firstName);
    this.profileForm.controls["lastName"].setValue(this.editProfileModel.lastName);
  }

  saveForm() {
    this.editProfileModel = this.profileForm.value;
    this.profileModel.firstName = this.editProfileModel.firstName;
    this.profileModel.lastName = this.editProfileModel.lastName;
    this.updateProfile();
  }
}



