import { Routes } from '@angular/router';
import { UserListComponent } from './pages/user-list/user-list.component';
import { userFormComponent } from './pages/user-form/user-form.component';

export const routes: Routes = [
  { path: '', component: UserListComponent },
  {
    path: 'create',
    component: userFormComponent,
  },
  { path: 'edit/:id', component: userFormComponent },
];
