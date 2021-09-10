import { CommentModel } from '../../_interfaces/comment/CommentModel';
import { AnswerToCommentModel } from '../../_interfaces/comment/answerToCommentModel';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { ProfileModel } from '../../_interfaces/profileModel';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { ToyModel } from 'src/app/_interfaces/toy/toyModel';
import { RepositoryService } from '../../shared/services/repository.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-get-comments',
  templateUrl: './get-comments.component.html',
  styleUrls: ['./get-comments.component.css']
})
export class GetCommentsComponent implements OnInit {

  public comments: CommentModel[];
  public profileModel: ProfileModel = {
    firstName: '',
    lastName:  '',
    email: ''
  }
  public isUserAuthenticated: boolean;
  public isUserAdmin : boolean;
  public answersToComment : AnswerToCommentModel;
  public addCommentForm: FormGroup;
  public addAnswerToCommentForm: FormGroup;
  public errorMessage = '';
  public showError: boolean;
  public isAnswerToComment: boolean;
  public isAnswerToCommentId: string;
  spin: boolean;
  isEmpty: boolean;

  constructor(private repository: RepositoryService,
     private authService: AuthenticationService,
     private _router: Router,
     private fb: FormBuilder
     ) {
       this.spin = true;
    this.isUserAuthenticated = this.authService.isUserAuthenticated();
    if(this.isUserAuthenticated)
    {
      this.isUserAdmin = this.authService.isUserAdmin();
      this.getProfile();
    }
   }

  ngOnInit() {
    this.spin =true;
    this.getComments();
    this.addCommentForm = this.fb.group({
      text: ['']   
    });
    this.addAnswerToCommentForm= this.fb.group({
      text: ['']   
    });
    
  }

  public formatDate = (date:Date) => {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear(),
        hour ='' + d.getHours(),
        minutes = ''+ d.getMinutes();

    if (month.length < 2) 
        month = '0' + month;
    if (day.length < 2) 
        day = '0' + day;
    if (hour.length < 2) 
        hour = '0' + hour;
    if (minutes.length < 2)
        minutes = '0' + minutes;    

    return [day, month,year ].join('/') +' ' + [hour, minutes].join(':');
}

  public getComments = () => {
    const apiAddress = 'api/comments';
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.comments = res as CommentModel[];
        this.spin = false;
        if(this.comments.length==0)
        {
          this.isEmpty=true;
        this.spin = false;
        }
      });
  }

  public AnswerToComment(id: string)
  {
    this.isEmpty=false;
    this.isAnswerToComment = true;
    this.isAnswerToCommentId = id;
  }

  public addAnswerToComment=(AnswerToCommentText:string, id:string) => {
    if(!this.isUserAuthenticated)
    {
      this.showError = true;
      this.errorMessage = "Будь ласка, спочатку авторизуйтеся!";
      return;
    }
    this.isEmpty=false;
    this.showError = false;
    var currentDate = new Date();
    currentDate.toJSON();    
    const answerToComment: AnswerToCommentModel = {
      id: "00000000-0000-0000-0000-000000000000",
      userName: this.profileModel.email,
      text: AnswerToCommentText,
      date: currentDate,
      commentId: id
    };
    this.repository.create('api/comments/addAnswerToComment', answerToComment)
      .subscribe(_ => {
          this.getComments();
        },
        error => {
          this.errorMessage = error;
          this.showError = true;
        });
  }

  public getProfile = () => {
    const apiAddress = 'api/profile';
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.profileModel = res as ProfileModel;
      });
  }

  public addComment = (addCommentFormValue) => {
    if(!this.isUserAuthenticated)
    {
      this.showError = true;
      this.errorMessage = "Будь ласка, спочатку авторизуйтеся!";
      return;
    }
    this.isEmpty=false;
    this.showError = false;
    var currentDate = new Date();
    currentDate.toJSON();
    var answerToCommentModels: AnswerToCommentModel[]
    const formValues = { ...addCommentFormValue };    
    const comment: CommentModel = {
      id: "00000000-0000-0000-0000-000000000000",
      userName: this.profileModel.email,
      text: formValues.text,
      date: currentDate,
      answerToCommentModels: answerToCommentModels
    };
    
    this.repository.create('api/comments/addcomment', comment)
      .subscribe(_ => {
          this.getComments();
        },
        error => {
          this.errorMessage = error;
          this.showError = true;
        });
  }

  public canDeleteComment= (userName: string): boolean=>
  {
    if(this.profileModel.email==userName || this.isUserAdmin)
      return true;
    return false;
  }

  public deleteComment=(id: string)=>{
    const apiAddress = 'api/comments/delete?id='+id;
    this.repository.delete(apiAddress)
      .subscribe(()=>{
        this.spin = true;
        this.getComments();
      });
    
  }

  public deleteAnswerToComment=(id: string)=>{
    const apiAddress = 'api/comments/deleteAnswerToComment?id='+id;
    this.repository.delete(apiAddress)
      .subscribe(()=>this.getComments());
    
  }
}
