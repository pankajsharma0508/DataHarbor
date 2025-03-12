import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-tabs',
  imports: [CommonModule],
  templateUrl: './tabs.component.html',
  styleUrl: './tabs.component.scss',
  standalone: true
})
export class TabsComponent {
  @Input() tabs: Tab[] = [];
  @Input() selectedTab: Tab | undefined;
  @Output() selectedTabChange = new EventEmitter<Tab>();

  onClick(tab: Tab) {
    this.selectedTab = tab;
    this.selectedTabChange.next(this.selectedTab);
  }
}

export class Tab {
  constructor(public name: string) { }
}

export const TabNames = {
  SourceData: 'Source Data',
  OperatorData: 'Operator Data',
  ProcessedData: 'Processed Data',
  ProcessingLogs: 'Processing Logs'
}