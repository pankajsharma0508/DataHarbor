import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Declaration } from '../../../model/declaration';
import { Tab, TabNames } from '../tabs/tabs.component';
import { DeclarationStore } from '../services/declaration-store.service';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-declaration',
  templateUrl: './declaration.component.html',
  styleUrl: './declaration.component.css',
  standalone: false
})
export class DeclarationComponent {
  protected tabs: Tab[] = [
    new Tab(TabNames.SourceData),
    new Tab(TabNames.OperatorData),
    new Tab(TabNames.ProcessingLogs),
  ]
  protected selectedTab = this.tabs[0];
  protected requestId: string | undefined;
  protected declaration: Declaration = {};
  protected tabNames = TabNames

  constructor(private store: DeclarationStore, private route: ActivatedRoute, private notification: NotificationService) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.requestId = params.get('id') || '';
      this.loadRequest(this.requestId)
    });
  }

  async loadRequest(requestId: any) {
    this.declaration = await this.store.get(requestId);
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
    await this.store.put(this.declaration);
    this.notification.showSuccess(`Saved Successfully.`);
  }
}
