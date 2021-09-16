import { ChangeDetectionStrategy, Component, EventEmitter, forwardRef, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Beneficiary } from '../shared/beneficiary.model';
import { RegisterService } from '../shared/register.service';

export interface BeneiciaryFormValues {
  startTime: string;
  dayOfWeek: string;
  beneficiaryId: string;
}

@Component({
  selector: 'app-beneficiary-form',
  templateUrl: './beneficiary-form.component.html',
  styleUrls: ['./beneficiary-form.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => BeneficiaryFormComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => BeneficiaryFormComponent),
      multi: true,
    }
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BeneficiaryFormComponent implements OnInit {

  daysOfTheWeekDropdown: string =''

  beneficiaryForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error: string;
  subscriptions: Subscription[] = [];

  constructor(private registerService: RegisterService, private formBuilder: FormBuilder) {
    this.beneficiaryForm = this.formBuilder.group({
      startTime: [],
      dayOfWeek: [], 
      beneficiaryId: []
    });

    this.subscriptions.push(
      this.beneficiaryForm.valueChanges.subscribe(value => {
        this.onChange(value);
        this.onTouched();
      })
    );
   }


   get value(): BeneficiaryFormComponent {
    return this.beneficiaryForm.value;
  }

  set value(value: BeneficiaryFormComponent) {
    this.beneficiaryForm.setValue(value);
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
    return this.beneficiaryForm.valid ? null : { beneficiary: { valid: false } };
  }

  dropDownDayOfWeek: string = '';
  selectedHandlerDayOfWeek(event : any)
  {
    if(event.target.value != 'default') 
      { this.dropDownDayOfWeek = event.target.value;}
    else 
      {this.dropDownDayOfWeek = '';}

      //console.log(this.dropDownRole);
  }


  @Output() dayOfWeekClickedEmitter = new EventEmitter();
  onDayOfWeekSelected(value:string){
    this.dayOfWeekClickedEmitter.emit(value);
  }

  selected(){
    return this.dropDownDayOfWeek;
  }



  ngOnInit(): void {
    this.onDayOfWeekSelected(this.dropDownDayOfWeek);
  }

}
