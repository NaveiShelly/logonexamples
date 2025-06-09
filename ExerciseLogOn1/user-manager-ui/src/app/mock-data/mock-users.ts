import { User } from '../models/user-item.model';

export const mockUsers: User[] = [
  {
    id: 1,
    name: 'Alice Cohen',
    email: 'alice@example.com',
    role: 'Admin',
    isActive: true,
    createdAt: new Date('2024-01-01'),
  },
  {
    id: 2,
    name: 'Bob Levi',
    email: 'bob@example.com',
    role: 'Editor',
    isActive: true,
    createdAt: new Date('2024-02-10'),
  },
  {
    id: 3,
    name: 'Charlie Mizrahi',
    email: 'charlie@example.com',
    role: 'Viewer',
    isActive: false,
    createdAt: new Date('2023-12-20'),
  },
  {
    id: 4,
    name: 'Dana Ben-David',
    email: 'dana@example.com',
    role: 'Editor',
    isActive: true,
    createdAt: new Date('2024-03-05'),
  },
  {
    id: 5,
    name: 'Eli Shapiro',
    email: 'eli@example.com',
    role: 'Viewer',
    isActive: true,
    createdAt: new Date('2024-04-15'),
  },
];
