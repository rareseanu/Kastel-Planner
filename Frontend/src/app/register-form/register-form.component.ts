import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonLabel } from '../shared/person-label.model';
import { RegisterService } from '../shared/register.service';

export interface LabelFormValues {
  id: string;
  labelName: string;
}

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegisterFormComponent implements OnInit {

  registerForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error: string;
  @Input()
  insertedPersonId: string;

  constructor( 
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private registerService: RegisterService) 
    
    {
    this.registerForm = this.formBuilder.group({
      person: new FormControl({ firstName: '', lastName: '', phoneNumber: '' }),
      label: new FormControl({ id: '', labelName: '' }),
      role: [],
      user: [],
      beneficiary:[]
    });
   }

   name:string = '';
   selectedRoleName= '';
   selectedRoleFromParentComponent:string ='';
   selectedDayOfWeekFromParentComponent:string ='';

   selectedLabelsArrayFromParentComponent: LabelFormValues[] = [];
   

  ngOnInit(): void {
  }


  showNextRoleComponent(value:string) {
    this.selectedRoleFromParentComponent = value;
    console.log(this.selectedRoleFromParentComponent);
  }

  showSelectedRoleName(value:string) : void {
    this.selectedRoleName = value;
    console.log("dsdas" + this.selectedRoleName);
  }

  showNextDayOfTheWeekComponent(value:string) {
    this.selectedDayOfWeekFromParentComponent = value;
    console.log(this.selectedDayOfWeekFromParentComponent);
  }

  showSelectedLabelsArray(value:LabelFormValues[]){
this.selectedLabelsArrayFromParentComponent = value;
console.log(this.selectedLabelsArrayFromParentComponent);

  }


onSubmit(){

  if (this.registerForm.invalid) {
    return;
  }

  //inserare persoana
    this.registerService.register(this.registerForm.get('person')?.value.firstName, this.registerForm.get('person')?.value.lastName, this.registerForm.get('person')?.value.phoneNumber, true)
    .subscribe(
      (data) => {
        this.insertedPersonId = data.id;
        console.log("perosn id from register()" + data.id);
          this.loading = false;  


          //inserare label
          for (let i=0; i<this.selectedLabelsArrayFromParentComponent.length;i++)
          {
            console.log("from register:" + " " + this.selectedLabelsArrayFromParentComponent[i].id);
            this.registerService.insertPersonLabel (this.selectedLabelsArrayFromParentComponent[i].id, this.insertedPersonId)
                .subscribe(
                    (data) => {
                                this.loading = false;
                     },
                    (error) => {
                              this.error = error;
                              this.loading = false;
                    });

          } 


          //inserare rol

          this.registerService.insertPersonRole (this.selectedRoleFromParentComponent,this.insertedPersonId)
          .subscribe(
            (data) => {
              console.log("perosn id from insertPersonRole" + data.personId);
              console.log("role id from insertPersonRole" + data.roleId);
                this.loading = false;
                 //daca rolul persoanei este de beneficiar
            if(this.selectedRoleFromParentComponent == 'ee352552-8ca0-4c7d-8907-ac7f7d95926d')
               {
                  this.registerService.insertWeeklyLog (this.registerForm.get('beneficiary')?.value.startTime, this.selectedDayOfWeekFromParentComponent, this.insertedPersonId)
                  .subscribe(
                       (data) => {
            
                                  this.loading = false;
                                  },
                      (error) => {
                                this.error = error;
                                this.loading = false;
              });
            }
            //daca rolul persoanei este de voluntar sau admin 
            else if (this.selectedRoleFromParentComponent == '49e177fb-7d43-41e9-b8f5-d7851c811434')
            {
               this.registerService.insertUserEmail (this.registerForm.get('user')?.value.email, this.insertedPersonId, "123456Az*")
          .subscribe(
            (data) => {
      
      
                this.loading = false;
            },
            (error) => {
                this.error = error;
                this.loading = false;
            });
      
            }
            else (this.selectedRoleFromParentComponent == 'fd42c927-402f-446c-a62a-12e3c8dda3d8')
            {
               this.registerService.insertUserEmail (this.registerForm.get('user')?.value.email, this.insertedPersonId, "123456Az*")
          .subscribe(
            (data) => {
          
      
                this.loading = false;
            },
            (error) => {
                this.error = error;
                this.loading = false;
            });
      
            }
                
      
            },
            (error) => {
                this.error = error;
                this.loading = false;
            });
            this.router.navigate(['/reset-password']);
          
      },
      (error) => {
          this.error = error;
          this.loading = false;
      });

      this.submitted = true;
 

 } 

}


