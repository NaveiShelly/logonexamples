import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, RouterModule, Router } from '@angular/router';
import { TaskService } from '../services/task.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './task-form.component.html',
})
export class TaskFormComponent implements OnInit {
  taskForm!: FormGroup;
  id?: number;

  constructor(
    private fb: FormBuilder,
    private service: TaskService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.taskForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: [''],
      isCompleted: [false],
      dueDate: [''],
    });

    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (this.id) {
      this.service.getTask(this.id).subscribe((task) => {
        this.taskForm.patchValue(task);
      });
    }
  }

  submit() {
    const formValue = this.taskForm.value;

    if (this.id) {
      this.service
        .updateTask(this.id, formValue)
        .subscribe(() => this.router.navigate(['/']));
    } else {
      this.service
        .createTask(formValue)
        .subscribe(() => this.router.navigate(['/']));
    }
  }
}
