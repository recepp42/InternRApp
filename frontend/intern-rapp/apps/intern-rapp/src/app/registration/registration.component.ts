import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TranslateModule } from '@ngx-translate/core';
import { AuthService } from '../services/auth-service.service';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { isValidSubmitPassword } from './customValidator';
import { Router } from '@angular/router';

@Component({
  selector: 'intern-rapp-registration',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    TranslateModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent implements OnInit {
  public RegistrationForm: FormGroup | undefined;
  constructor(private authService: AuthService,private router:Router) {}
  ngOnInit(): void {
    this.RegistrationForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      submitPassword: new FormControl('', [Validators.required,isValidSubmitPassword()]),
    });
  }
  register() {
    this.authService.register(this.RegistrationForm?.getRawValue()).subscribe();
    this.router.navigateByUrl("/login")    
  }
}
