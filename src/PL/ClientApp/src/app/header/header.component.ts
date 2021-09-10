import { Router } from '@angular/router';
import { AuthenticationService } from './../shared/services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  public isUserAuthenticated: boolean;
  public token;
  public searchForm: FormGroup;

  constructor(
    private _authService: AuthenticationService,
    private _router: Router,
    private fb: FormBuilder
    ) {
    this._authService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      });
  }

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      name: ['']
    });
  }

  public logout = () => {
    this._authService.logout();
    this._router.navigate(['/']);
  }

  public search=(searchFormValue)=>{
    const formValues = { ...searchFormValue };
    this._router.navigate(['/search',searchFormValue.name]);
  }

}
