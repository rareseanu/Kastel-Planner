import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, EventEmitter, forwardRef, Input, OnInit, Output } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Label } from '../shared/label.model';
import { FormArray, FormBuilder, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR } from '@angular/forms';
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

  
  labelsInDropdown : any[];
  labelForm: FormGroup;
  selectedLabels: any[] = [];;
  dropdownSettings = {};

  loading = false;
  submitted = false;
  returnUrl: string;
  error: string;
  subscriptions: Subscription[] = [];
  formControls: any;

  get labelsFormArray() {
    return this.labelForm.controls.labels as FormArray;
  }

  constructor(private registerService: RegisterService, private formBuilder: FormBuilder) {
    this.displayLabels();
    
      this.labelForm = this.formBuilder.group({
        id:[],
        labelName:[]
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
          //console.log(this.labelsInDropdown);
        }
      } );
    
    }
  

  @Output() selectedLabelsClickedEmitter = new EventEmitter();
  onSelectedLabelsArray(value:string[]){
    this.selectedLabelsClickedEmitter.emit(value);
  }

  ngOnInit(): void {
     this.displayLabels();
     this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'labelName'
    };
    
  
     
  }

  onItemSelect(item: any) {
    this.selectedLabels.push(item);
    this.onSelectedLabelsArray(this.selectedLabels);
   // console.log(this.selectedLabels);
  }

  onItemDeSelect(item: any) {
    let index = this.selectedLabels.indexOf(item);
     this.selectedLabels.splice(index, 1);
     this.onSelectedLabelsArray(this.selectedLabels);
   // console.log(this.selectedLabels);
  }

 

  }


