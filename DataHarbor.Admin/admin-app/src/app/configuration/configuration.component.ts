import { Component } from '@angular/core';
import { Tab, TabsComponent } from '../tabs/tabs.component';
import { CommonModule } from '@angular/common';
import { ProcessingConfiguration } from '../../model/processingConfiguration';
import { ActivatedRoute } from '@angular/router';
import { ConfigurationStore } from '../configuration.service';
import { FileConfigurationComponent } from '../file-configuration/file-configuration.component';
import { MappingConfigurationComponent } from '../mapping-configuration/mapping-configuration.component';

@Component({
  selector: 'app-configuration',
  imports: [CommonModule, TabsComponent, FileConfigurationComponent, MappingConfigurationComponent],
  templateUrl: './configuration.component.html',
  styleUrl: './configuration.component.scss',
  standalone: true
})
export class ConfigurationComponent {
  protected tabs: Tab[] = [
    new Tab('File Settings'),
    new Tab('Mapping')
  ]

  protected selectedTab = this.tabs[0];
  protected configurationId: string | undefined;
  protected configuration: ProcessingConfiguration | undefined;


  constructor(private store: ConfigurationStore,
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.configurationId = params.get('id') || '';
      this.loadConfiguration(this.configurationId)
    });

  }

  async loadConfiguration(id: string) {
    try {
      this.configuration = await this.store.get(id)
    } catch (ex) {
    }
  }

}
