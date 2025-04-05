import { Component } from '@angular/core';
import { ProcessingConfiguration } from '../../../model/models';
import { ConfigurationService } from '../../../api/configuration.service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-configurations',
  templateUrl: './configurations.component.html',
  styleUrl: './configurations.component.css',
  standalone: false
})
export class ConfigurationsComponent {
  protected configurations: Array<ProcessingConfiguration> = [];

  constructor(private service: ConfigurationService) {

  }

  ngOnInit(): void {
    this.loadConfigurations();
  }

  async loadConfigurations() {
    const config = await lastValueFrom(this.service.apiConfigurationAllGet());
    this.configurations = config;
  }

}
