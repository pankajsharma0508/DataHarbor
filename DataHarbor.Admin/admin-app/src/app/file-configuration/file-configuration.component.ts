import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FilesConfigurations } from '../../model/filesConfigurations';
import { DxNumberBoxModule, DxSelectBoxModule, DxSwitchModule, DxTextBoxModule } from 'devextreme-angular';
import { FileColumnDelimiter, FileDecimalSymbol, FileFormat, FileLineDelimiter, FileThousandSeparator } from './file-configuration-contants';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-file-configuration',
  imports: [CommonModule, DxSelectBoxModule, DxTextBoxModule, FormsModule, DxNumberBoxModule, DxSwitchModule],
  templateUrl: './file-configuration.component.html',
  styleUrl: './file-configuration.component.scss'
})
export class FileConfigurationComponent {
  @Input() fileConfiguration?: FilesConfigurations;
  protected FileFormats = FileFormat.FileLineDelimiters;
  protected LineDelimiters = FileLineDelimiter.FileLineDelimiters;
  protected ColumnDelimiters = FileColumnDelimiter.FileColumnDelimiters;
  protected DecimalSymbols = FileDecimalSymbol.FileDecimalSymbols;
  protected ThousandSeparators = FileThousandSeparator.FileThousandSeparators;

}
