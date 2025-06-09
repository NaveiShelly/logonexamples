import { Routes } from '@angular/router';
import { TaskListComponent } from './pages/task-list.component';
import { TaskFormComponent } from './pages/task-form.component';

export const routes: Routes = [
  { path: '', component: TaskListComponent },
  { path: 'create', component: TaskFormComponent },
  { path: 'edit/:id', component: TaskFormComponent },
];
