import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

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
}

export class Tab {
  constructor(public name: string) {

  }
}