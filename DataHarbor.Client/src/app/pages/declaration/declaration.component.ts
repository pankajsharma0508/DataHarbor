import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Declaration } from '../../../model/declaration';
import { Tab, TabNames } from '../tabs/tabs.component';
import { DeclarationStore } from '../services/declaration-store.service';
import { NotificationService } from '../services/notification.service';
import { ProcessingSteps, ProcessStepMessage } from './declaration-constants';
import { DxDropDownButtonTypes } from 'devextreme-angular/ui/drop-down-button';
import { ProcessService } from '../../../api/process.service';
import { ProcessMessage } from '../../../model/processMessage';
import { lastValueFrom } from 'rxjs';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import { ExportService } from '../../shared/services/export.service';
import { LoadPanelService } from '../../shared/services';


@Component({
  selector: 'app-declaration',
  templateUrl: './declaration.component.html',
  styleUrl: './declaration.component.css',
  standalone: false
})
export class DeclarationComponent {
  protected tabs: Tab[] = [
    new Tab(TabNames.SourceData),
    new Tab(TabNames.TransactionData),
    new Tab(TabNames.ProcessingLogs),
  ]
  protected selectedTab = this.tabs[0];
  protected requestId: string | undefined;
  protected declaration: Declaration = {};
  protected tabNames = TabNames
  protected steps = ProcessingSteps;

  constructor(private store: DeclarationStore,
    private route: ActivatedRoute,
    private processService: ProcessService,
    private exportService: ExportService,
    private loadService: LoadPanelService,
    private notification: NotificationService) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.requestId = params.get('id') || '';
      this.loadRequest(this.requestId)
    });
  }

  async loadRequest(requestId: any) {
    try {
      this.loadService.show();
      this.declaration = await this.store.get(requestId);
    } catch (ex) {
      this.notification.showError('Error while loading..');
    } finally {
      this.loadService.hide();
    }
  }

  get rawData() {
    return this.declaration.rawData || [];
  }
  get transactions() {
    return this.declaration.transactions || [];
  }
  get processingLogs() {
    return this.declaration.processingLogs || [];
  }

  async saveDeclaration() {
    try {
      await this.store.put(this.declaration);
      this.notification.showSuccess(`Saved Successfully.`);
    } catch (e) {
      this.notification.showError(`Unable to save declaration ${this.declaration.name}.`);
    }
  }

  async onItemClick(e: DxDropDownButtonTypes.ItemClickEvent) {
    const stepMessage = e.itemData as ProcessStepMessage;
    const request = <ProcessMessage>{};
    request.declarationId = this.requestId;
    request.status = stepMessage.message;
    await lastValueFrom(this.processService.apiProcessSendMessagePost(request));
    this.notification.showSuccess(`Processing files ${stepMessage.message}.`);
  }

  onExporting(e: DxDataGridTypes.ExportingEvent) {
    this.exportService.onExporting(this.selectedTab.name, e);
  }
}
