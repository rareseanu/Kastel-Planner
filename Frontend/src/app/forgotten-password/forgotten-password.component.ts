import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';

import { AuthenticationService } from '../shared/authentication.service'
import { ToastService } from '../toast/toast.service';

@Component({
    templateUrl: 'forgotten-password.component.html'
})
export class ForgottenPasswordComponent {
    loginForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error: string;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private toastService: ToastService
    ) {
    }

    emailValidator(nameRe: RegExp): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
          const forbidden = nameRe.test(control.value);
          return forbidden ? {forbiddenName: {value: control.value}} : null;
        };
    }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]]
        });

        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';        
    }

    get f() { return this.loginForm.controls; }

    onSubmit() {
        this.submitted = true;

        if (this.loginForm.invalid) {
            return;
        }

        this.loading = true;
        this.authenticationService.forgotPassword(this.f.email.value)
            .subscribe(
                (data) => {
                    this.loading = false;
                    this.router.navigate(['password-reset']);
                    this.toastService.info("Check your email inbox.")
                },
                (error) => {
                    this.error = error;
                    this.loading = false;
                });
    }
}