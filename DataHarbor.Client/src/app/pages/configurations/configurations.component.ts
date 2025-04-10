import { Component } from '@angular/core';
import { FilesConfigurations, ProcessingConfiguration } from '../../../model/models';
import { ConfigurationService } from '../../../api/configuration.service';
import { lastValueFrom } from 'rxjs';
import { RowInsertedEvent, RowRemovedEvent, RowUpdatedEvent } from 'devextreme/ui/data_grid';
import { NotificationService } from '../services/notification.service';
import { Guid } from 'devextreme/common';
import { FileColumnDelimiter, FileDecimalSymbol, FileFormat, FileLineDelimiter, FileThousandSeparator } from '../configuration/file-configuration/file-configuration-contants';
import { ExportService } from '../../shared/services/export.service';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';

@Component({
  selector: 'app-configurations',
  templateUrl: './configurations.component.html',
  styleUrl: './configurations.component.css',
  standalone: false
})
export class ConfigurationsComponent {
  protected configurations: Array<ProcessingConfiguration> = [];

  constructor(private service: ConfigurationService,
    private exportService: ExportService,
    private notification: NotificationService) {
  }

  ngOnInit(): void {
    this.loadConfigurations();
  }

  async loadConfigurations() {
    const config = await lastValueFrom(this.service.apiConfigurationAllGet());
    this.configurations = config;
  }

  async onRowRemoved(event: RowRemovedEvent) {
    try {
      const fc = event.data as ProcessingConfiguration;
      if (fc?.id) {
        await lastValueFrom(this.service.apiConfigurationIdDelete(fc.id.toString()));
        await this.loadConfigurations();
        this.notification.showSuccess(`${fc.name} deleted successfully.`);
      }
    } catch (e) {
      this.notification.showError(`Unable to delete configuration.`);
    }
  }

  async onRowInserted(event: RowInsertedEvent) {
    try {
      const fc = event.data as ProcessingConfiguration;
      fc.id = fc.id || new Guid().toString();
      fc.uniqueId = fc.id;
      fc.modifiedOn = new Date();
      this.initConfiguration(fc);
      await lastValueFrom(this.service.apiConfigurationCreatePost(fc));
      await this.loadConfigurations();
      this.notification.showSuccess(`${fc.name} created successfully.`);
    } catch (e) {
      this.notification.showError(`Unable to create configuration.`);
    }
  }

  async onRowUpdated(event: RowUpdatedEvent) {
    try {
      const fc = event.data as ProcessingConfiguration;
      fc.modifiedOn = new Date();
      await lastValueFrom(this.service.apiConfigurationUpdatePost(fc));
      await this.loadConfigurations();
      this.notification.showSuccess(`${fc.name} updated successfully.`);
    } catch (e) {
      this.notification.showError(`Unable to update configuration.`);
    }
  }

  initConfiguration(fc: ProcessingConfiguration) {
    fc.mailboxFileName = '';
    fc.mailboxFilePath = '';
    fc.layoutMappings = [];
    const fileConfiguration = <FilesConfigurations>{};
    fileConfiguration.fileCategory = "Transaction File";
    fileConfiguration.fileFormat = FileFormat.Formats[0].extension;
    fileConfiguration.columnSeparator = FileColumnDelimiter.FileColumnDelimiters[0].columnDelimiter;
    fileConfiguration.digitalGroupSeparator = FileThousandSeparator.FileThousandSeparators[0].format;
    fileConfiguration.decimalSeparator = FileColumnDelimiter.FileColumnDelimiters[0].columnDelimiter;
    fileConfiguration.lineSeparator = FileLineDelimiter.FileLineDelimiters[0].lineDelimiter;
    fileConfiguration.headerRowsToIgnore = 0;
    fileConfiguration.footerRowsToIgnore = 0;
    fileConfiguration.textQualifier = "";
    fc.operatorFilesConfigurations = fileConfiguration;
  }
  onExporting(e: DxDataGridTypes.ExportingEvent) {
    this.exportService.onExporting('Configurations', e);
  }
}
