import { Component, OnInit } from '@angular/core';
import { TaskService } from '../services/task.service';
import { AsyncPipe, CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TaskItem } from '../models/task-item.model';
import { Observable } from 'rxjs'; // ✅ חובה כדי ש-Observable יעבוד

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [AsyncPipe, RouterModule, CommonModule],
  templateUrl: './task-list.component.html',
})
export class TaskListComponent implements OnInit {
  tasks$!: Observable<TaskItem[]>;

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.tasks$ = this.taskService.tasks$;
  }

  deleteTask(id: number) {
    this.taskService.deleteTask(id).subscribe();
  }
}
