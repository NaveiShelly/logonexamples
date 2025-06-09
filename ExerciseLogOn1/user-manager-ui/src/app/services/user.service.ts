import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { User } from '../models/user-item.model';
import { Observable, BehaviorSubject, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UserService {
  private baseUrl = 'http://localhost:5072';

  private userSubject = new BehaviorSubject<User[]>([]);
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadUsers();
  }

  loadUsers() {
    this.http.get<User[]>(`${this.baseUrl}/users`).subscribe((users) => {
      this.userSubject.next(users);
    });
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/users/${id}`);
  }

  addUser(user: User): Observable<User> {
    return this.http
      .post<User>(`${this.baseUrl}/users`, user)
      .pipe(tap(() => this.loadUsers()));
  }

  updateUser(id: number, user: User): Observable<{ message: string }> {
    return this.http
      .put<{ message: string }>(`${this.baseUrl}/users/${id}`, user)
      .pipe(tap(() => this.loadUsers()));
  }

  deleteUser(id: number): Observable<{ message: string }> {
    return this.http
      .delete<{ message: string }>(`${this.baseUrl}/users/${id}`)
      .pipe(tap(() => this.loadUsers()));
  }

  setUsers(users: User[]) {
    this.userSubject.next(users);
  }

  filterUsers(
    name?: string,
    role?: string,
    isActive?: boolean
  ): Observable<User[]> {
    let params = new HttpParams();

    if (name) params = params.set('name', name);
    if (role) params = params.set('role', role);
    if (isActive !== undefined)
      params = params.set('isActive', isActive.toString());

    return this.http.get<User[]>(`${this.baseUrl}/users/filters`, { params });
  }
}
