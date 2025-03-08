import { Component, OnInit } from '@angular/core';
import { Tab, TabsComponent } from '../tabs/tabs.component';
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
export class DeclarationComponent implements OnInit{
  protected tabs: Tab[] = [
    new Tab('Details'),
    new Tab('Processing Logs'),
  ]
  protected selectedTab = this.tabs[0];
  protected requestId: string | undefined;
  protected declaration: Declaration = {};

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

  get entries () {
    return this.declaration.data || [];
  }
}
