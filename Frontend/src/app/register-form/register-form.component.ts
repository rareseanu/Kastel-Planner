import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonLabel } from '../shared/person-label.model';
import { RegisterService } from '../shared/register.service';
import { WeeklyLogService } from '../shared/weekly-log.service';
import { ToastService } from '../toast/toast.service';

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
    private registerService: RegisterService,
    private toastService: ToastService,
    private weeklyLogService: WeeklyLogService) {
    this.registerForm = this.formBuilder.group({
      person: new FormControl({ firstName: '', lastName: '', phoneNumber: '' }),
      label: new FormControl({ id: '', labelName: '' }),
      role: [],
      user: [],
      beneficiary: []
    });
  }

  name: string = '';
  selectedRoleName = '';
  selectedRoleFromParentComponent: string = '';
  selectedDayOfWeekFromParentComponent: string = '';

  selectedLabelsArrayFromParentComponent: LabelFormValues[] = [];


  ngOnInit(): void {
  }


  showNextRoleComponent(value: string) {
    this.selectedRoleFromParentComponent = value;
    console.log(this.selectedRoleFromParentComponent);
  }

  showSelectedRoleName(value: string): void {
    this.selectedRoleName = value;
    console.log("dsdas" + this.selectedRoleName);
  }

  showNextDayOfTheWeekComponent(value: string) {
    this.selectedDayOfWeekFromParentComponent = value;
    console.log(this.selectedDayOfWeekFromParentComponent);
  }

  showSelectedLabelsArray(value: LabelFormValues[]) {
    this.selectedLabelsArrayFromParentComponent = value;
    console.log(this.selectedLabelsArrayFromParentComponent);

  }


  onSubmit() {

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
          for (let i = 0; i < this.selectedLabelsArrayFromParentComponent.length; i++) {
            console.log("from register:" + " " + this.selectedLabelsArrayFromParentComponent[i].id);
            this.registerService.insertPersonLabel(this.selectedLabelsArrayFromParentComponent[i].id, this.insertedPersonId)
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

          this.registerService.insertPersonRole(this.selectedRoleFromParentComponent, this.insertedPersonId)
            .subscribe(
              (data) => {
                console.log("perosn id from insertPersonRole" + data.personId);
                console.log("role id from insertPersonRole" + data.roleId);
                console.log(this.selectedRoleName);
                this.loading = false;
                //daca rolul persoanei este de beneficiar
                if (this.selectedRoleName === 'Beneficiary') {
                  this.weeklyLogService.createWeeklyLog(this.registerForm.get('beneficiary')?.value.startTime, this.selectedDayOfWeekFromParentComponent, this.registerForm.get('beneficiary')?.value.duration, this.insertedPersonId)
                    .subscribe(
                      (data) => {
                        this.toastService.success("User created successfully!");
                        this.loading = false;
                      },
                      (error) => {
                        this.error = error;
                        this.loading = false;
                      });
                }
                //daca rolul persoanei este de voluntar sau admin 
                else if (this.selectedRoleName === 'Volunteer') {
                  this.registerService.insertUserEmail(this.registerForm.get('user')?.value.email, this.insertedPersonId, "123456Az*")
                    .subscribe(
                      (data) => {
                        this.toastService.success("User created successfully!");
                        this.loading = false;
                      },
                      (error) => {
                        this.toastService.danger(error);
                        this.error = error;
                        this.loading = false;
                      });

                }
                else if (this.selectedRoleName === 'Admin')
                {
                  this.registerService.insertUserEmail(this.registerForm.get('user')?.value.email, this.insertedPersonId, "123456Az*")
                    .subscribe(
                      (data) => {
                        this.toastService.success("User created successfully!");
                        this.loading = false;
                      },
                      (error) => {
                        this.error = error;     
                        this.toastService.danger(error);
                        this.loading = false;
                      });

                }


              },
              (error) => {
                this.error = error;
                this.loading = false;
              });

        },
        (error) => {
          this.error = error;
          this.loading = false;
        });

    this.submitted = true;

  }

}


