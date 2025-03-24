import { Component, Input } from '@angular/core';
import { ProcessingConfiguration } from '../../model/processingConfiguration';
import { CommonModule } from '@angular/common';
import { DxDataGridModule } from 'devextreme-angular';

@Component({
  selector: 'app-mapping-configuration',
  imports: [CommonModule, DxDataGridModule],
  templateUrl: './mapping-configuration.component.html',
  styleUrl: './mapping-configuration.component.scss'
})
export class MappingConfigurationComponent {
  @Input() configuration?: ProcessingConfiguration;


  get mappings() {
    return this.configuration?.layoutMappings || [];
  }
}
