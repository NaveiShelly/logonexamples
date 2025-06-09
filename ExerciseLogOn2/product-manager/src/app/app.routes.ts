import { Routes } from '@angular/router';
import path from 'path';
import { ProductList } from './components/product-list';
import { ProductForm } from './components/product-form';

export const routes: Routes = [
  { path: '', component: ProductList },
  {
    path: 'create',
    component: ProductForm,
  },
  { path: 'edit/:id', component: ProductForm },
];
