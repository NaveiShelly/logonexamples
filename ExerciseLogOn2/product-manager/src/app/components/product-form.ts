import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';
import { ProductService } from '../services/product.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-form',
  standalone: true,
  templateUrl: './product-form.html',
  styleUrls: ['./product-form.css'],
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
})
export class ProductForm implements OnInit {
  productForm!: FormGroup;
  id?: number;

  constructor(
    private fb: FormBuilder,
    private service: ProductService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      price: [null, [Validators.required, Validators.min(0.01)]],
      category: ['', Validators.required],
      inStock: [true],
    });

    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (this.id) {
      this.service.getProduct(this.id).subscribe((product) => {
        this.productForm.patchValue(product);
      });
    }
  }

  submit() {
    const formValue = this.productForm.value;

    if (this.id) {
      this.service
        .updateProduct(this.id, formValue)
        .subscribe(() => this.router.navigate(['/']));
    } else {
      this.service
        .addProduct(formValue)
        .subscribe(() => this.router.navigate(['/']));
    }
  }
}
