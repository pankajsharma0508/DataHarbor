import { Component, Input } from '@angular/core';
import { ProcessingConfiguration } from '../../../../model/processingConfiguration';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import { ExportService } from '../../../shared/services/export.service';

@Component({
  selector: 'app-mapping-configuration',
  templateUrl: './mapping-configuration.component.html',
  standalone: false
})
export class MappingConfigurationComponent {
  @Input() configuration?: ProcessingConfiguration;

  constructor(private exportService: ExportService) {
  }

  protected fieldTypes = [
    { text: 'Text', value: 'string' },
    { text: 'Date', value: 'date' },
    { text: 'Number', value: 'number' },
    { text: 'Decimal', value: 'decimal' }
  ]

  get mappings() {
    return this.configuration?.layoutMappings || [];
  }
  onExporting(e: DxDataGridTypes.ExportingEvent) {
    this.exportService.onExporting('ColumnMappings', e);
  }
}
