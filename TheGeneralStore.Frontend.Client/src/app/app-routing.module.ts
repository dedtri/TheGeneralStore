import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ProductListComponent } from './components/product/product-list/product-list.component';
import { CategoryListComponent } from './components/category/category-list/category-list.component';
import { ImageListComponent } from './components/image/image-list/image-list.component';
import { ProductDetailsComponent } from './components/product/product-details/product-details.component';
import { CartComponent } from './components/cart/cart.component';
import { CategoryProductListComponent } from './components/category/category-product-list/category-product-list.component';
import { LoginComponent } from './components/login/login/login.component';
import { AuthGuard } from './services/authguard.service';
import { RegisterComponent } from './components/login/register/register.component';
import { CheckOutComponent } from './components/check-out/check-out.component';
import { ThankyouComponent } from './components/thankyou/thankyou.component';
import { UserprofileComponent } from './components/login/userprofile/userprofile.component';
import { MyorderComponent } from './components/order/myorder/myorder.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'products', component: ProductListComponent, canActivate: [AuthGuard] },
  { path: 'products/:id', component: ProductDetailsComponent},
  { path: 'categories/:categoryName/:id', component: ProductDetailsComponent},
  { path: 'categories', component: CategoryListComponent},
  { path: 'product-images', component: ImageListComponent },
  { path: 'cart', component: CartComponent },
  { path: 'categories/:categoryName', component: CategoryProductListComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'checkout', component: CheckOutComponent, canActivate: [AuthGuard] },
  { path: 'thankyou', component: ThankyouComponent},
  { path: 'dashboard/:id', component: UserprofileComponent, canActivate: [AuthGuard]},
  { path: 'dashboard/:id/myorders', component: MyorderComponent, canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }