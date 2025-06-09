export interface Product {
  id?: number;
  name: string;
  price: number;
  category: 'Electronics' | 'Clothing' | 'Books';
  inStock: boolean;
}
