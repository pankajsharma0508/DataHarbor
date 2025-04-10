import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ConfigurationService } from '../../../api/configuration.service';
import { DeclarationService } from '../../../api/declaration.service';
import { ProcessService } from '../../../api/process.service';
import { Declaration } from '../../../model/declaration';
import { ProcessingConfiguration } from '../../../model/processingConfiguration';
import { NotificationService } from '../services/notification.service';
import { Guid } from 'devextreme/common';
import { RowRemovedEvent } from 'devextreme/ui/data_grid';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import { ExportService } from '../../shared/services/export.service';
import { LoadPanelService } from '../../shared/services';


@Component({
  selector: 'app-declarations',
  templateUrl: './declarations.component.html',
  styleUrl: './declarations.component.css',
  standalone: false
})
export class DeclarationsComponent implements OnInit {
  showPopup = false;
  protected selectedConfig: ProcessingConfiguration | undefined;
  protected filePath: string = '';
  protected description: string = '';
  configurations: Array<ProcessingConfiguration> = [];
  protected declarations: Declaration[] = [];

  constructor(private service: DeclarationService,
    private configurationService: ConfigurationService,
    private notification: NotificationService,
    private exportService: ExportService,
    private loadService: LoadPanelService,
    private processService: ProcessService) {
  }

  ngOnInit(): void {
    try {
      this.loadService.show();
      this.loadConfigurations();
      this.loadDeclarations();
    } catch (ex) {
      this.notification.showError('Error while loading..');
    } finally {
      this.loadService.hide();
    }
  }

  async loadConfigurations() {
    const configOptions = await lastValueFrom(this.configurationService.apiConfigurationAllGet());
    this.configurations = configOptions;
  }

  async loadDeclarations() {
    const declarations = await lastValueFrom(this.service.apiDeclarationAllGet());
    this.declarations = declarations;
  }

  async announce() {
    try {
      const declaration = <Declaration>{};
      declaration.uniqueId = new Guid().toString();
      declaration.filePath = this.filePath;
      declaration.name = this.selectedConfig?.name;
      declaration.description = this.description || '';
      const newDeclaration = await lastValueFrom(this.service.apiDeclarationCreatePost(declaration));
      this.declarations.push(newDeclaration);
      this.notification.showSuccess('Declaration recorded successfully.');
      this.showPopup = false;
    } catch (e) {
      this.notification.showSuccess('Unable to record declaration.');
    }
  }

  async onRowRemoved(event: RowRemovedEvent) {
    try {
      const declaration = event.data as Declaration;
      if (declaration?.uniqueId) {
        await lastValueFrom(this.service.apiDeclarationIdDelete(declaration?.uniqueId));
        await this.loadDeclarations();
        this.notification.showSuccess(`${declaration.name} deleted successfully.`);
      }
    } catch (e) {
      this.notification.showError(`Unable to delete declaration.`);
    }
  }
  onExporting(e: DxDataGridTypes.ExportingEvent) {
    this.exportService.onExporting('Declarations', e);
  }
}
