import { Component } from '@angular/core';
import { Tab, TabsComponent } from '../tabs/tabs.component';
import { CommonModule } from '@angular/common';
import { ProcessingConfiguration } from '../../model/processingConfiguration';
import { ConfigurationApi } from '../configuration.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-configuration',
  imports: [CommonModule, TabsComponent],
  templateUrl: './configuration.component.html',
  styleUrl: './configuration.component.scss',
  standalone: true
})
export class ConfigurationComponent {
  protected tabs: Tab[] = [
    new Tab('Details'),
    new Tab('File Settings'),
    new Tab('Mapping')
  ]

  protected selectedTab = this.tabs[0];
  protected configurationId: string | undefined;
  protected configuration: ProcessingConfiguration | undefined;


  constructor(private api: ConfigurationApi,
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
      this.configuration = await this.api.get(id)
    } catch (ex) {
    }
  }

}
