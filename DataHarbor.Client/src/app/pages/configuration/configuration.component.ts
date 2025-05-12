import { Component, ElementRef, ViewChild } from '@angular/core';
import { LayoutMapping, ProcessingConfiguration } from '../../../model/models';
import { ActivatedRoute } from '@angular/router';
import { Categories } from './configuration-constants';
import { ConfigurationStore } from '../services/configuration.service';
import { Tab } from '../tabs/tabs.component';
import { NotificationService } from '../services/notification.service';
import FileSaver from 'file-saver';

@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrl: './configuration.component.css',
  standalone: false
})
export class ConfigurationComponent {
  protected tabs: Tab[] = [
    new Tab('General'),
    new Tab('File Settings'),
    new Tab('Mapping')
  ]

  protected selectedTab = this.tabs[0];
  protected configurationId: string | undefined;
  protected configuration: ProcessingConfiguration = {};
  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  constructor(private store: ConfigurationStore,
    private notification: NotificationService,
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
      this.configuration.attachments = [];
      await this.store.put(this.configuration);
      this.notification.showSuccess(`Saved Successfully.`);
    } catch (ex) {
    }
  }

  exportConfiguration() {
    const blob = new Blob([JSON.stringify(this.configuration, null, 2)], { type: 'application/json' });
    FileSaver.saveAs(blob, `${this.configuration.name}-configuration.json`);
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
        this.configuration.mailboxFilePath = snri?.mailboxFilePath;
        this.configuration.mailboxFileName = snri?.mailboxFileName;
      }
    } else {
      configuration.name = this.configuration.name;
      configuration.id = this.configuration.id;
      configuration.description = this.configuration.description;
      configuration.uniqueId = this.configuration.uniqueId;
      this.configuration = configuration;
    }
    this.saveConfiguration();
  }
}
