import { ChangeDetectionStrategy, Component, forwardRef, OnDestroy, OnInit } from '@angular/core';
import { ControlValueAccessor, FormBuilder, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

export interface PersonFormValues {
  firstName: string;
  lastName: string;
  phoneNumber: number;
}

@Component({
  selector: 'app-person-form',
  templateUrl: './person-form.component.html',
  styleUrls: ['./person-form.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PersonFormComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => PersonFormComponent),
      multi: true,
    }
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PersonFormComponent implements OnInit, ControlValueAccessor, OnDestroy {

  personForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error: string;
    subscriptions: Subscription[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router) {
      const PAT_NAME = "^[a-zA-Z]{2,20}$";
      this.personForm = this.formBuilder.group({
        firstName: ['', [Validators.required, Validators.pattern(PAT_NAME)]],
        lastName: ['', [Validators.required, Validators.pattern(PAT_NAME)]],
        phoneNumber: []
     });

     this.subscriptions.push(
      this.personForm.valueChanges.subscribe(value => {
        this.onChange(value);
        this.onTouched();
      })
    );
    }
  ngOnInit(): void {
    
   
  }



  get value(): PersonFormValues {
    return this.personForm.value;
  }

  set value(value: PersonFormValues) {
    this.personForm.setValue(value);
    this.onChange(value);
    this.onTouched();
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
    return this.personForm.valid ? null : { person: { valid: false } };
  }
}
