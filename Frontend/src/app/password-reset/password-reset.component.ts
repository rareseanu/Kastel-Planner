import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AuthenticationService } from '../shared/authentication.service'
import { ToastService } from '../toast/toast.service';

@Component({
    templateUrl: 'password-reset.component.html'
})
export class PasswordResetComponent {
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

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            email: ['', Validators.required],
            token: ['', Validators.required],
            password: ['', Validators.required]
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
        this.authenticationService.resetPassword(this.f.token.value, this.f.email.value, this.f.password.value)
            .subscribe(
                (data) => {
                    this.loading = false;
                    this.toastService.success("Password changed successfully.");
                },
                (error) => {
                    this.error = error;
                    this.loading = false;
                });
    }
}