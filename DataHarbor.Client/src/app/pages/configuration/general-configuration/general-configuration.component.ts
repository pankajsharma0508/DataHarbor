import { Component, Input } from '@angular/core';
import { ProcessingConfiguration } from '../../../../model/processingConfiguration';

@Component({
  selector: 'app-general-configuration',
  standalone: false,
  templateUrl: './general-configuration.component.html',
  styleUrl: './general-configuration.component.scss'
})
export class GeneralConfigurationComponent {
  @Input() configuration?: ProcessingConfiguration;
}
