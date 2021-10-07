import { Component } from "@angular/core";
import { ToastService } from "./toast.service";

@Component({
    selector: 'toast',
    templateUrl: './toast.component.html',
    styleUrls: ['./toast.component.css']
})

export class ToastComponent {
    constructor(public toastService: ToastService) {}
}