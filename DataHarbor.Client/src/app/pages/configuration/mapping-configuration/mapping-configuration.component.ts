import { Component, Input } from '@angular/core';
import { ProcessingConfiguration } from '../../../../model/processingConfiguration';

@Component({
  selector: 'app-mapping-configuration',
  templateUrl: './mapping-configuration.component.html',
  standalone: false
})
export class MappingConfigurationComponent {
  @Input() configuration?: ProcessingConfiguration;

  protected fieldTypes = [
    { text: 'Text', value: 'string' },
    { text: 'Date', value: 'date' },
    { text: 'Number', value: 'number' },
    { text: 'Decimal', value: 'decimal' }
  ]

  get mappings() {
    return this.configuration?.layoutMappings || [];
  }
}
