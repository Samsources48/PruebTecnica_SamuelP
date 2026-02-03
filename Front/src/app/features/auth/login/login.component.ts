import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthStore } from '../../../core/auth/auth.store';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private authStore = inject(AuthStore);

  showPassword = false;
  rememberMe = false;

  loginForm = this.fb.group({
    email: ['admin', [Validators.required]],
    password: ['admin123', [Validators.required]]
  });

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      this.authStore.login({
        userName: email!,
        password: password!
      });
    }
  }
}

