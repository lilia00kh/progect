import { AuthGuard } from './shared/guards/auth.guard';
import { ErrorHandlerService } from './shared/services/error-handler.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { JwtModule } from '@auth0/angular-jwt';
import {MatButtonModule} from '@angular/material/button';
import {MatMenuModule} from '@angular/material/menu';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import {BasketComponent} from './basket/basket.component'
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatRadioModule} from '@angular/material/radio';
import { HeaderComponent } from './header/header.component';
import { SearchComponent } from './search/search.component';
import { RxReactiveFormsModule } from '@rxweb/reactive-form-validators';
import { FooterComponent } from './footer/footer.component';
import { AboutCompanyComponent } from './about-company/about-company.component';
import { AboutPaymentComponent } from './about-payment/about-payment.component';
import { NotFoundGoodComponent } from './error-pages/not-found-good/not-found-good.component';
import { SuccessOrderComponent } from './success-pages/success-order/success-order.component';

// import {MDBBootstrapModule} from 'angular-bootstrap-md';

export function tokenGetter() {
  return localStorage.getItem('token');
}
const routes: Routes = [
  { path: 'home', component: HomeComponent },
      { path: 'order', loadChildren: () => import('./order/order.module').then(m => m.OrderModule) },
      { path: 'good', loadChildren: () => import('./good/good.module').then(m => m.GoodModule) },
      { path: 'search/:name', component: SearchComponent},
      { path: 'comment', loadChildren: () => import('./comment/comment.module').then(m => m.CommentModule) },
      { path: 'profile', loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule), canActivate: [AuthGuard] },
      { path: 'basket', component: BasketComponent, canActivate: [AuthGuard] },
      { path: 'about-company', component: AboutCompanyComponent },
      { path: 'about-payment', component: AboutPaymentComponent },
      { path: 'authentication', loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule) },
      { path: 'forbidden', component: ForbiddenComponent },
      { path: 'not-found', component : NotFoundGoodComponent},
      { path: 'success-order', component : SuccessOrderComponent},
      { path: '404', component : NotFoundComponent},
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: '**', redirectTo: '/404', pathMatch: 'full'},
];
@NgModule({
  declarations: [
    SuccessOrderComponent,
    SearchComponent,
    AppComponent,
    HomeComponent,
    FooterComponent,
    HeaderComponent,
    MenuComponent,
    NotFoundComponent,
    NotFoundGoodComponent,
    ForbiddenComponent,
    BasketComponent,
    AboutCompanyComponent,
    AboutPaymentComponent
  ],
  imports: [
    BrowserModule,
    RxReactiveFormsModule,
    FormsModule, 
    MatProgressSpinnerModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatButtonModule,
    MatMenuModule,
    NgbModule,
    MatRadioModule,
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'enabled', 
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5001'],
        blacklistedRoutes: []
      }
    }),
    BrowserAnimationsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
