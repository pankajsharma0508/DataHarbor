import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { ConfigurationService } from '../api/configuration.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'admin-app';
  data: any;
  constructor(private service: ConfigurationService) {
    this.service.apiConfigurationConfigurationAllGet('W644').toPromise().then(data => {
      this.data = data;
    });
  }
}
