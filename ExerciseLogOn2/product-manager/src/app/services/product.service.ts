import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../models/product-item.model';
import { Observable, BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseUrl = 'http://localhost:5100';

  private ProductSubject = new BehaviorSubject<Product[]>([]);
  product$ = this.ProductSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadProducts();
  }

  loadProducts() {
    this.http
      .get<Product[]>(`${this.baseUrl}/Products`)
      .subscribe((Products) => {
        this.ProductSubject.next(Products);
      });
  }

  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/Products/${id}`);
  }

  addProduct(Product: Product): Observable<Product> {
    return this.http
      .post<Product>(`${this.baseUrl}/Products`, Product)
      .pipe(tap(() => this.loadProducts()));
  }

  updateProduct(id: number, Product: Product): Observable<{ message: string }> {
    return this.http
      .put<{ message: string }>(`${this.baseUrl}/Products/${id}`, Product)
      .pipe(tap(() => this.loadProducts()));
  }

  deleteProduct(id: number): Observable<{ message: string }> {
    return this.http
      .delete<{ message: string }>(`${this.baseUrl}/Products/${id}`)
      .pipe(tap(() => this.loadProducts()));
  }
}
