import { Injectable } from '@angular/core';
import notify from 'devextreme/ui/notify';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor() { }
  showNotification(message: string, type = 'error') {
    notify({
      message: message,
      height: 45,
      width: 300,
      minWidth: 150,
      type: type,
      displayTime: 3500,
      animation: {
        show: {
          type: 'fade', duration: 400, from: 0, to: 1,
        },
        hide: { type: 'fade', duration: 40, to: 0 },
      },
      position: "bottom center",
      direction: "up-push"
    });
  }

  showError(message: string) {
    this.showNotification(message, 'error');
  }
  showSuccess(message: string) {
    this.showNotification(message, 'success');
  }
  showInfo(message: string) {
    this.showNotification(message, 'info');
  }
  showWarning(message: string) {
    this.showNotification(message, 'warning');
  }
}

