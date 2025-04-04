import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Declaration } from '../../../model/declaration';
import { Tab, TabNames } from '../tabs/tabs.component';
import { DeclarationStore } from '../services/declaration-store.service';

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
    new Tab(TabNames.ProcessedData),
  ]
  protected selectedTab = this.tabs[0];
  protected requestId: string | undefined;
  protected declaration: Declaration = {};
  protected tabNames = TabNames

  constructor(private store: DeclarationStore, private route: ActivatedRoute) {
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

  saveDeclaration() {
    this.store.put(this.declaration);
  }
}
