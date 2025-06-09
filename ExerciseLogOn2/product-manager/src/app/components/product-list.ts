import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product-item.model';
import { ProductService } from '../services/product.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-list.html',
  styleUrls: ['./product-list.css'],
})
export class ProductList implements OnInit {
  product$!: Observable<Product[]>;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.product$ = this.productService.product$;
  }

  deleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe();
  }

  trackById(index: number, product: Product): number {
    return product.id!;
  }
}
