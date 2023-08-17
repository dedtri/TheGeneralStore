import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
    ProductCreateDetailsComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
