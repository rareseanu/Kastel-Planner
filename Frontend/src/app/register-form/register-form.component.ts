import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
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

  constructor( 
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private registerService: RegisterService) 
    
    {
    this.registerForm = this.formBuilder.group({
      person: []
    //  label: []
    });
   }
   

  ngOnInit(): void {
  }


  onSubmit() {
    this.registerService.register(this.registerForm.get('person')?.value.firstName, this.registerForm.get('person')?.value.lastName, this.registerForm.get('person')?.value.phoneNumber, true)
    .subscribe(
      (data) => {
          this.loading = false;
      },
      (error) => {
          this.error = error;
          this.loading = false;
      });

      /*this.registerService.insertPersonLabel("b8b57560-025f-492c-a7ac-435d86ca18c3", "89083b49-bf0d-4be7-bcb2-853836e5081f")
    .subscribe(
      (data) => {
          this.loading = false;
      },
      (error) => {
          this.error = error;
          this.loading = false;
      });*/

}
}
