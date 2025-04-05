import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ConfigurationService } from '../../../api/configuration.service';
import { DeclarationService } from '../../../api/declaration.service';
import { ProcessService } from '../../../api/process.service';
import { Anchored } from '../../../model/anchored';
import { Declaration } from '../../../model/declaration';
import { ProcessingConfiguration } from '../../../model/processingConfiguration';
import { NotificationService } from '../services/notification.service';
import { Guid } from 'devextreme/common';
import { RowRemovedEvent } from 'devextreme/ui/data_grid';


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
  configurations: Array<ProcessingConfiguration> = [];
  protected requests: Declaration[] = [];

  constructor(private service: DeclarationService,
    private configurationService: ConfigurationService,
    private notification: NotificationService,
    private processService: ProcessService) {
  }

  ngOnInit(): void {
    this.loadConfigurations();
    this.loadDeclarations();
  }

  async loadConfigurations() {
    const configOptions = await lastValueFrom(this.configurationService.apiConfigurationAllGet());
    this.configurations = configOptions;
  }

  async loadDeclarations() {
    const requests = await lastValueFrom(this.service.apiDeclarationAllGet());
    this.requests = requests;
  }

  async announce() {
    try {
      const msg = <Anchored>{};
      msg.uniqueId = new Guid().toString();
      msg.filePath = this.filePath;
      msg.name = this.selectedConfig?.name;
      await lastValueFrom(this.processService.apiProcessSendMessagePost(msg));
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
}
