import { Component, OnInit } from '@angular/core';
import { CommonModule, AsyncPipe } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserService } from '../../services/user.service';
import { Observable } from 'rxjs';
import { User } from '../../models/user-item.model';
import { ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterModule, ReactiveFormsModule],
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
  user$!: Observable<User[]>;
  filterForm!: FormGroup;
  constructor(private fb: FormBuilder, private userService: UserService) {}

  ngOnInit(): void {
    this.user$ = this.userService.user$;
    this.filterForm = this.fb.group({
      name: [''],
      role: [''],
      isActive: [''],
    });
  }

  deleteUser(id: number) {
    this.userService.deleteUser(id).subscribe();
  }

  activateUser(id: number) {
    this.userService.getUser(id).subscribe((user) => {
      if (user) {
        user.isActive = !user.isActive;
        this.userService.updateUser(id, user).subscribe();
      }
    });
  }

  submit() {
    const { name, role, isActive } = this.filterForm.value;
    const parsedIsActive = isActive === '' ? undefined : isActive === 'true';

    this.userService
      .filterUsers(name, role, parsedIsActive)
      .subscribe((users) => this.userService.setUsers(users));
  }

  resetFilter() {
    this.filterForm.reset();
    this.userService.loadUsers();
  }
}
