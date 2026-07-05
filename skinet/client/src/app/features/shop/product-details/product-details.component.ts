import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../shared/models/product';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatFormField, MatLabel } from '@angular/material/select';
import { MatInput } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';
import { CartService } from '../../../core/services/cart.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-details',
  imports: [
    CurrencyPipe,
    MatButton,
    MatIcon,
    MatFormField,
    MatInput,
    MatLabel,
    MatDivider,
    FormsModule,
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss',
})
export class ProductDetailsComponent implements OnInit {
  private shopService = inject(ShopService);
  private activatedRoute = inject(ActivatedRoute);
  product?: Product;
  quantityInCart = 0;
  quantity = 0;
  private cartService = inject(CartService);
  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;
    this.shopService.getPdoduct(+id).subscribe({
      next: (product) => {
        this.product = product;
        this.updateQuantityInCart();
      },
      error: (error) => console.log(error),
    });
  }

  updateCart() {
    if (!this.product) return;
    if (this.quantity > 0) {
      const itemstoAdd = this.quantity;
      this.quantityInCart += itemstoAdd;
      this.cartService.addItemtoCart(this.product, itemstoAdd);
    }
    //else {
    //const itemsToremove = (this.quantityInCart = this.quantity);
    //this.quantityInCart -= itemsToremove;
    //this.cartService.removeItemFromcart(this.product.id, itemsToremove);
    //}
  }

  updateQuantityInCart() {
    this.quantityInCart =
      this.cartService.cart()?.items.find((x) => x.productId === this.product?.id)?.quantity || 0;

    this.quantity = this.quantityInCart;
  }

  getButtonText() {
    return this.quantityInCart > 0 ? 'update cart' : 'Add to cart';
  }
}
