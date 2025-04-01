import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DxDataGridModule } from 'devextreme-angular';
import { ConfigurationService } from '../../api/configuration.service';
import { lastValueFrom } from 'rxjs';
import { ProcessingConfiguration } from '../../model/processingConfiguration';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-configuration-list',
  imports: [CommonModule, DxDataGridModule, RouterModule],
  templateUrl: './configuration-list.component.html',
  styleUrl: './configuration-list.component.scss',
  standalone: true
})
export class ConfigurationListComponent implements OnInit {
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
