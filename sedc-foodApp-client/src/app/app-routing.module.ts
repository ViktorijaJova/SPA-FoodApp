import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { ForbiddenComponent } from './components/forbidden/forbidden.component';
import { HomeComponent } from './components/home/home.component';
import { OrderComponent } from './components/order/order.component';
import { RestaurantDetailsComponent } from './components/restaurant-details/restaurant-details.component';
import { RestaurantMenuComponent } from './components/restaurant-menu/restaurant-menu.component';
import { RestaurantsComponent } from './components/restaurants/restaurants.component';
import { ChangePasswordComponent } from './user/change-password/change-password.component';
import { ForgotPasswordComponent } from './user/forgot-password/forgot-password.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { ResetPasswordComponent } from './user/reset-password/reset-password.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
    { path:'', redirectTo:'/home', pathMatch:'full' },
    { path:'home', component: HomeComponent },
    { path:'admin-panel', component: AdminPanelComponent, canActivate:[AuthGuard], data :{permittedRoles: ["ADMIN"]} },
    { path:'restaurant-details/:id', component: RestaurantDetailsComponent },
    { path:'restaurants', component: RestaurantsComponent},
    { path:'restaurants/restaurant-menu/:id', component: RestaurantMenuComponent},
    { path:'order', component: OrderComponent},
    { path:'forbidden', component: ForbiddenComponent},
    {
        path: 'user', component: UserComponent,
        children: [
            { path:'register', component: RegisterComponent },
            { path:'login', component: LoginComponent },
        ]
    },
    { path: 'user/change-password', component: ChangePasswordComponent},
    { path: 'user/forgot-password', component: ForgotPasswordComponent},
    { path: 'user/reset-password', component: ResetPasswordComponent},
]

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }