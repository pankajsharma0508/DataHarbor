import { Component, OnInit } from '@angular/core';
import { Tab, TabsComponent } from '../tabs/tabs.component';
import { CommonModule } from '@angular/common';
import { ConfigurationApi } from '../configuration.service';
import { ProcessingConfiguration } from '../../model/processingConfiguration';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-declaration',
  imports: [CommonModule, TabsComponent],
  templateUrl: './declaration.component.html',
  styleUrl: './declaration.component.scss',
  standalone: true
})
export class DeclarationComponent {
  protected tabs: Tab[] = [
    new Tab('Details'),
    new Tab('Processing Logs'),
  ]
  protected selectedTab = this.tabs[0];

 
}
