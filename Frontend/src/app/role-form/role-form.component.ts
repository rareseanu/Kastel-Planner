import { ChangeDetectionStrategy, Component, EventEmitter, forwardRef, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Subscription } from 'rxjs';
import { RegisterService } from '../shared/register.service';
import { Role } from '../shared/role.model';

export interface RoleFormValues {
  id: string;
  roleName: string;
}

@Component({
  selector: 'app-role-form',
  templateUrl: './role-form.component.html',
  styleUrls: ['./role-form.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => RoleFormComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => RoleFormComponent),
      multi: true,
    }
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RoleFormComponent implements OnInit {

  rolesInDropdown : Role[];

  roleForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error: string;
  subscriptions: Subscription[] = [];

  constructor(private registerService: RegisterService, private formBuilder: FormBuilder) {
    this.displayRoles();
    
      this.roleForm = this.formBuilder.group({
        id: [],
        roleName: []
      });

      this.subscriptions.push(
        this.roleForm.valueChanges.subscribe(value => {
          this.onChange(value);
          this.onTouched();
        })
      );
   }

   get value(): RoleFormComponent {
    return this.roleForm.value;
  }

  set value(value: RoleFormComponent) {
    this.roleForm.setValue(value);
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
    return this.roleForm.valid ? null : { role: { valid: false } };
  }

  displayRoles():void{  
    this.registerService.getRolesFromAPI().
     subscribe(data => {
       if(data) {
         this.rolesInDropdown = data;
         console.log(this.rolesInDropdown);
       }
     } );

   
  }

  //methods to get dropdown values
  dropDownRole: string = '';
  selectedRole: string ='';

  selectedHandlerRoleName(event : any)
  {
    if(event.target.value != 'default') 
      { this.dropDownRole = event.target.value;}
    else 
      {this.dropDownRole = '';}

     // console.log(this.dropDownLabelName);
  }

  @Output() roleClickedEmitter = new EventEmitter();
  onRoleSelected(value:string){
    this.roleClickedEmitter.emit(value);
  }

  selected(){
    return this.dropDownRole;
  }

  ngOnInit(): void {
    this.displayRoles();
    this.onRoleSelected(this.dropDownRole);
  }

}
