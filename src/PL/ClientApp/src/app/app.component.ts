import { Component } from '@angular/core';
import { Router, Event, NavigationEnd, NavigationStart, } from '@angular/router';
import { AuthenticationService } from './shared/services/authentication.service';
import { RepositoryService } from './shared/services/repository.service';
import { User } from './_interfaces/user.model';
import { UserToCreate } from './_interfaces/userToCreate.model';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  title = 'CompanyEmployees.Client';
  spin: boolean;
  constructor(private repository: RepositoryService,
    private _authService: AuthenticationService,
    private router: Router){
      this.router.events.subscribe((event: Event) => {
        if (event instanceof NavigationStart) {
          this.spin = true;
        }

        if (event instanceof NavigationEnd) {
          this.spin = false;
        }
      }
      );
    }
  ngOnInit(): void {
    if(this._authService.isUserAuthenticated())
      this._authService.sendAuthStateChangeNotification(true);
  }
  
  
}
