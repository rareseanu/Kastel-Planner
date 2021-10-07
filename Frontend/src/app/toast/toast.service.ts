import { Injectable } from "@angular/core";

export class Message {
    content: string;
    title: string;
    style: string;
    dismissed: boolean = false;

    constructor(content: string, style?: string) {
        this.content = content;
        this.style = style || 'info'
    }
}


@Injectable({ providedIn: 'root' })
export class ToastService {
    toasts: Message[] = [];
    toastCounter: 0;

    constructor() {}

    show(content: string, style?: string) {
        let toast = new Message(content, style || 'info');
        this.toasts.push(toast);
        ++this.toastCounter;
        setTimeout(() => {
            this.removeToast(toast)
        }, 3000);
    }

    removeToast(toast: Message) {
        if (!this.toasts.includes(toast)) return;

        this.toasts = this.toasts.filter(x => x !== toast);
    }

    success(content:string) {
        this.show(content, "success")
    }

    info(content:string) {
        this.show(content, "info");
    }

    danger(content:string) {
        this.show(content, "danger");
    }

}