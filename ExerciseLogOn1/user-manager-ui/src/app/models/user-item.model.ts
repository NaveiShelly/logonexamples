export interface User {
  id: number;
  name: string;
  email: string;
  role: 'Admin' | 'Editor' | 'Viewer';
  isActive: boolean;
  createdAt: Date;
}
