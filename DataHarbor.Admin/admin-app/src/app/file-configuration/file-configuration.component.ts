import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FilesConfigurations } from '../../model/filesConfigurations';

@Component({
  selector: 'app-file-configuration',
  imports: [CommonModule],
  templateUrl: './file-configuration.component.html',
  styleUrl: './file-configuration.component.scss'
})
export class FileConfigurationComponent {
  @Input() fileConfiguration: FilesConfigurations | undefined;

}
