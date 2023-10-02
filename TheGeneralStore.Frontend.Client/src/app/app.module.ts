import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from "@auth0/angular-jwt";

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ProductListComponent } from './components/product/product-list/product-list.component';
import { ProductCreateComponent } from './components/product/product-create/product-create.component';
import { ProductDetailsComponent } from './components/product/product-details/product-details.component';
import { CategoryListComponent } from './components/category/category-list/category-list.component';
import { CategoryCreateComponent } from './components/category/category-create/category-create.component';
import { CategoryUpdateComponent } from './components/category/category-update/category-update.component';
import { ImageListComponent } from './components/image/image-list/image-list.component';
import { ImageUploadComponent } from './components/image/image-upload/image-upload.component';
import { ProductCreateDetailsComponent } from './components/product/product-create/product-create-details/product-create-details.component';
import { PaginationComponent } from './components/shared/pagination/pagination.component';
import { CartComponent } from './components/cart/cart.component';
import { HomeComponent } from './components/home/home.component';
import { CategoryProductListComponent } from './components/category/category-product-list/category-product-list.component';
import { LoginComponent } from './components/login/login/login.component';
import { AuthGuard } from './services/authguard.service';
import { RegisterComponent } from './components/login/register/register.component';
import { UserprofileComponent } from './components/login/userprofile/userprofile.component';
import { CheckOutComponent } from './components/check-out/check-out.component';
import { ThankyouComponent } from './components/thankyou/thankyou.component';
import { MyorderComponent } from './components/order/myorder/myorder.component';

export function tokenGetter() { 
  return localStorage.getItem("jwt"); 
}

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    ProductListComponent,
    ProductCreateComponent,
    ProductDetailsComponent,
    CategoryListComponent,
    CategoryCreateComponent,
    CategoryUpdateComponent,
    ImageListComponent,
    ImageUploadComponent,
    ProductCreateDetailsComponent,
    PaginationComponent,
    CartComponent,
    HomeComponent,
    CategoryProductListComponent,
    LoginComponent,
    RegisterComponent,
    UserprofileComponent,
    CheckOutComponent,
    ThankyouComponent,
    MyorderComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7113"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
