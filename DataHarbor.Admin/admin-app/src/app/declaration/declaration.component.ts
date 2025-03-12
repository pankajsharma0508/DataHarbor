import { Component, OnInit } from '@angular/core';
import { Tab, TabNames, TabsComponent } from '../tabs/tabs.component';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Declaration } from '../../model/declaration';
import { DeclarationStore } from '../declaration-store.service';
import { DxDataGridModule } from 'devextreme-angular';

@Component({
  selector: 'app-declaration',
  imports: [CommonModule, TabsComponent, DxDataGridModule],
  templateUrl: './declaration.component.html',
  styleUrl: './declaration.component.scss',
  standalone: true
})
export class DeclarationComponent implements OnInit {
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
