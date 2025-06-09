import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TaskItem } from '../models/task-item.model';
import { Observable, BehaviorSubject, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class TaskService {
  private baseUrl = 'http://localhost:5290';

  private tasksSubject = new BehaviorSubject<TaskItem[]>([]);
  tasks$ = this.tasksSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadTasks();
  }

  private loadTasks() {
    this.http.get<TaskItem[]>(`${this.baseUrl}/tasks`).subscribe((tasks) => {
      this.tasksSubject.next(tasks);
    });
  }

  getTask(id: number): Observable<TaskItem> {
    return this.http.get<TaskItem>(`${this.baseUrl}/tasks/${id}`);
  }

  createTask(task: TaskItem): Observable<TaskItem> {
    return this.http
      .post<TaskItem>(`${this.baseUrl}/tasks`, task)
      .pipe(tap(() => this.loadTasks()));
  }

  updateTask(id: number, task: TaskItem): Observable<any> {
    return this.http
      .put(`${this.baseUrl}/tasks/${id}`, task)
      .pipe(tap(() => this.loadTasks()));
  }

  deleteTask(id: number): Observable<any> {
    return this.http
      .delete(`${this.baseUrl}/tasks/${id}`)
      .pipe(tap(() => this.loadTasks()));
  }
}
