import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCreateDetailsComponent } from './product-create-details.component';

describe('ProductCreateDetailsComponent', () => {
  let component: ProductCreateDetailsComponent;
  let fixture: ComponentFixture<ProductCreateDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductCreateDetailsComponent]
    });
    fixture = TestBed.createComponent(ProductCreateDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
