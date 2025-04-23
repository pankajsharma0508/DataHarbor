import { Component, ElementRef, ViewChild } from '@angular/core';
import { Tab, TabsComponent } from '../tabs/tabs.component';
import { CommonModule } from '@angular/common';
import { ProcessingConfiguration } from '../../model/processingConfiguration';
import { ActivatedRoute } from '@angular/router';
import { ConfigurationStore } from '../configuration.service';
import { FileConfigurationComponent } from '../file-configuration/file-configuration.component';
import { MappingConfigurationComponent } from '../mapping-configuration/mapping-configuration.component';
import { DxFileUploaderModule } from 'devextreme-angular';
import { LayoutMapping } from '../../model/layoutMapping';
import { Categories } from './configuration-constants';

@Component({
  selector: 'app-configuration',
  imports: [CommonModule, TabsComponent, FileConfigurationComponent, MappingConfigurationComponent, DxFileUploaderModule],
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
  protected configuration: ProcessingConfiguration = {};
  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

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

  triggerFileInput() {
    this.fileInput.nativeElement.click(); // Triggers the hidden file input
  }

  async saveConfiguration() {
    try {
      await this.store.put(this.configuration);
    } catch (ex) {
    }
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];

      if (file.type === 'application/json') {
        const reader = new FileReader();
        reader.onload = (e) => {
          const fileContent = e.target?.result;
          console.log('JSON File Content:', fileContent);
          this.readConfigurationJson(fileContent);
        };
        reader.readAsText(file);
      } else {
        alert('Please select a valid JSON file.');
      }
    }
  }

  readConfigurationJson(fileText:any) {
    const configuration = JSON.parse(fileText);
    const snri = configuration?.snriConfiguration;
    if (snri) {
      const mailbox = snri.layoutMappings.find((x: { category: string; }) => x.category === Categories.Mailbox);
      const transactionFiles = snri.layoutMappings.find((x: { category: string; }) => x.category === Categories.TransactionFiles);
      if (this.configuration) {
        this.configuration.layoutMappings = new Array<LayoutMapping>;
        const mergedMapping = transactionFiles.mappings.map((x: any) => {
          const mapping = <LayoutMapping>{}
          mapping.id = x.id;
          mapping.fieldOrder = x.fieldOrder;
          mapping.sourceColumn = x.sourceColumn;

          const targetMapping = mailbox.mappings.find((m: any) => m.fieldName === x.fieldName);
          if (targetMapping) {
            mapping.fieldName = targetMapping.sourceColumn;
            mapping.fieldType = targetMapping.fieldType;
            mapping.isUnique = targetMapping.isUnique;
            mapping.formatPattern = targetMapping.formatPattern || '';
          }
          return mapping;
        });
        this.configuration.layoutMappings = mergedMapping;
      }
    }

    this.saveConfiguration();
  }
}
