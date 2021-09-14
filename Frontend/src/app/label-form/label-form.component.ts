import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, forwardRef, Input, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Label } from '../shared/label.model';
import { FormBuilder, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR } from '@angular/forms';
import { RegisterService } from '../shared/register.service';

export interface LabelFormValues {
  id: string;
  labelName: string;
}

@Component({
  selector: 'app-label-form',
  templateUrl: './label-form.component.html',
  styleUrls: ['./label-form.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => LabelFormComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => LabelFormComponent),
      multi: true,
    }
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LabelFormComponent implements OnInit {

  @Input()
  labelsInDropdown : Label[];

  types$: any;

  labelForm: FormGroup;
  loading = false;
    submitted = false;
    returnUrl: string;
    error: string;
    subscriptions: Subscription[] = [];

  constructor(private registerService: RegisterService, private formBuilder: FormBuilder,) {
    this.displayLabels();
    
      this.labelForm = this.formBuilder.group({
        id: [],
        labelName: []
      });

      this.subscriptions.push(
        this.labelForm.valueChanges.subscribe(value => {
          this.onChange(value);
          this.onTouched();
        })
      );
     
   }

   get value(): LabelFormComponent {
    return this.labelForm.value;
  }

  set value(value: LabelFormComponent) {
    this.labelForm.setValue(value);
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
    return this.labelForm.valid ? null : { label: { valid: false } };
  }


  displayLabels():void{  
     this.registerService.getLabelsFromAPI().
      subscribe(data => {
        if(data) {
          this.labelsInDropdown = data;
          console.log(this.labelsInDropdown);
        }
      } );

    
    }

  //methods to get dropdown values
  dropDownLabelName: string = '';
  selectedHandlerLabelName(event : any)
  {
    if(event.target.value != 'default') 
      { this.dropDownLabelName = event.target.value;}
    else 
      {this.dropDownLabelName = '';}

      console.log(this.dropDownLabelName);
  }


  selectedLabel(){
    return this.dropDownLabelName;
  }

  ngOnInit(): void {
     this.displayLabels();
  }
  }
