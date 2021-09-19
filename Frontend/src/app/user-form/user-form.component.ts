import { ChangeDetectionStrategy, Component, forwardRef, OnInit } from '@angular/core';
import { ControlValueAccessor, FormBuilder, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { RegisterService } from '../shared/register.service';
import { matchingInputsValidator } from './validators';

export interface InsertUserFormValues {
  email: string;
  personId: string;
  password: string;
}

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => UserFormComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => UserFormComponent),
      multi: true,
    }
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserFormComponent implements OnInit, ControlValueAccessor {

  userForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error: string;
  subscriptions: Subscription[] = [];

  constructor(private registerService: RegisterService,private formBuilder: FormBuilder) {
    this.userForm = this.formBuilder.group({
      email: [],
      personId: [],
      password:[]
    });

    this.subscriptions.push(
      this.userForm.valueChanges.subscribe(value => {
        this.onChange(value);
        this.onTouched();
      })
    );
   }


   get value(): UserFormComponent {
    return this.userForm.value;
  }

  set value(value: UserFormComponent) {
    this.userForm.setValue(value);
    this.onChange(value);
    this.onTouched();
  }

  get passwordControl() {
    return this.userForm.controls.password;
  }

  get confirmPasswordControl() {
    return this.userForm.controls.confirmPassword;
  }



  onChange: any = () => {};
  onTouched: any = () => {};

  writeValue(value: any) {
    if (value) {
      this.value = value;
    } 
  }

  ngOnDestroy() {
    this.subscriptions.forEach(s => s.unsubscribe());
  }
  registerOnChange(fn: any) {
    this.onChange = fn;
  }

  registerOnTouched(fn: any) {
    this.onTouched = fn;
  }

  // communicate the inner form validation to the parent form
  validate(_: FormControl) {
    return this.userForm.valid ? null : { user: { valid: false } };
  }


  ngOnInit(): void {
  }

}


