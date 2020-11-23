import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AliveService } from './services/alive.service';
import { HomeComponent } from './components/home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { AdminPanelService } from './services/admin-panel.service';
import { ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { MapMunicipalityPipe } from './pipes/map-municipality.pipe';
import { RestaurantDetailsComponent } from './components/restaurant-details/restaurant-details.component';
import { MapMealTypePipe } from './pipes/map-mealType.pipe';
import { IsVegePipe } from './pipes/is-vege.pipe';
import { RestaurantsComponent } from './components/restaurants/restaurants.component';
import { RestaurantService } from './services/restaurant.service';
import { RestaurantComponent } from './components/restaurant/restaurant.component';
import { RestaurantMenuComponent } from './components/restaurant-menu/restaurant-menu.component';
import { RestaurantMenuItemComponent } from './components/restaurant-menu-item/restaurant-menu-item.component';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { UserService } from './services/user.service';
import { LoaderComponent } from './shared/loader/loader.component';
import { AuthService } from './auth/auth.service';
import { ForbiddenComponent } from './components/forbidden/forbidden.component';
import { ChangePasswordComponent } from './user/change-password/change-password.component';
import { ForgotPasswordComponent } from './user/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './user/reset-password/reset-password.component';
import { OrderService } from './services/order.service';
import { OrderComponent } from './components/order/order.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AdminPanelComponent,
    HeaderComponent,
    FooterComponent,
    MapMunicipalityPipe,
    MapMealTypePipe,
    IsVegePipe,
    RestaurantDetailsComponent,
    RestaurantsComponent,
    RestaurantComponent,
    RestaurantMenuComponent,
    RestaurantMenuItemComponent,
    UserComponent,
    LoginComponent,
    RegisterComponent,
    LoaderComponent,
    ForbiddenComponent,
    ChangePasswordComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    OrderComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    ModalModule.forRoot()
  ],
  providers: [
    AliveService,
    AdminPanelService,
    RestaurantService,
    UserService,
    AuthService,
    OrderService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
