import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RegisterService } from '../shared/register.service';

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
      person: [],
      label: [],
      role: [],
      user: [],
      beneficiary:[]
    });
   }

   name:string = '';
   selectedRoleFromParentComponent:string ='';
   selectedDayOfWeekFromParentComponent:string ='';
   selectedLabelsArrayFromParentComponent: string[] = [];
   

  ngOnInit(): void {
  }


  showNextRoleComponent(value:string) {
    this.selectedRoleFromParentComponent = value;
    console.log(this.selectedRoleFromParentComponent);
  }

  showNextDayOfTheWeekComponent(value:string) {
    this.selectedDayOfWeekFromParentComponent = value;
    console.log(this.selectedDayOfWeekFromParentComponent);
  }

  showSelectedLabelsArray(value:string[]){
this.selectedLabelsArrayFromParentComponent = value;
console.log(this.selectedLabelsArrayFromParentComponent);

  }


onSubmit(){


   /* this.registerService.register(this.registerForm.get('person')?.value.firstName, this.registerForm.get('person')?.value.lastName, this.registerForm.get('person')?.value.phoneNumber, true)
    .subscribe(
      (data) => {
          this.loading = false;
          console.log("perosn id from register()" + data.id);
          this.insertedPersonId = data.id;
      },
      (error) => {
          this.error = error;
          this.loading = false;
      });


      
    this.registerService.insertPersonRole (this.selectedRoleFromParentComponent,this.insertedPersonId)
    .subscribe(
      (data) => {
        console.log("perosn id from insertPersonRole" + data.personId);
        console.log("role id from insertPersonRole" + data.roleId);
          this.loading = false;
      },
      (error) => {
          this.error = error;
          this.loading = false;
      });


   /*this.registerService.insertPersonLabel ("b8b57560-025f-492c-a7ac-435d86ca18c3","89083b49-bf0d-4be7-bcb2-853836e5081f")
    .subscribe(
      (data) => {
          this.loading = false;
      },
      (error) => {
          this.error = error;
          this.loading = false;
      }); */

    this.registerService.insertWeeklyLog (this.registerForm.get('beneficiary')?.value.startTime, this.selectedDayOfWeekFromParentComponent, "a93fb416-d6a6-4a34-a0fd-8e570cac2f54")
    .subscribe(
      (data) => {
    
          this.loading = false;
      },
      (error) => {
          this.error = error;
          this.loading = false;
      });
 } 

}


