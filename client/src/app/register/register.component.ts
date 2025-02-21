import { Component, EventEmitter, inject, input, Input, OnInit, output, Output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-register',
  // imports: [FormsModule],
  imports: [ReactiveFormsModule, JsonPipe],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);

  // @Input() userFromHomeComponent: any;
  // userFromHomeComponent = input.required<any>()
  // @Output() cancelRegister = new EventEmitter();
  cancelRegister = output<boolean>();
  model: any = {}
  registerForm: FormGroup = new FormGroup({});

  ngOnInit(): void {
    this.initializeForm()
  }

  initializeForm() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]),
      confirmPassword: new FormControl('',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]),
    })
  }

  register() {
    console.log(this.registerForm.value);
    // this.accountService.register(this.model).subscribe({
    //   next: response => {
    //     console.log(response);
    //     this.cancel();
    //   },
    //   error: error => this.toastr.error(error.error),
    // })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
