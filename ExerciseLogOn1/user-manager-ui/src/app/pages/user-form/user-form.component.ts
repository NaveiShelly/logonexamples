import { Component, OnInit } from '@angular/core';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-form',
  standalone: true,
  templateUrl: './user-form.component.html',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
})
export class userFormComponent implements OnInit {
  userForm!: FormGroup;
  id?: number;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.userForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      role: ['', Validators.required],
    });

    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (this.id) {
      this.userService.getUser(this.id).subscribe((user) => {
        this.userForm.patchValue(user);
      });
    }
  }

  submit() {
    const formValue = this.userForm.value;
    if (this.id) {
      this.userService
        .updateUser(this.id, formValue)
        .subscribe(() => this.router.navigate(['/']));
    } else {
      this.userService
        .addUser(formValue)
        .subscribe(() => this.router.navigate(['/']));
    }
  }
}
