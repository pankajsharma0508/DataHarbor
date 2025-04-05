import { Component, Input } from '@angular/core';
import { FileColumnDelimiter, FileDecimalSymbol, FileFormat, FileLineDelimiter, FileThousandSeparator } from './file-configuration-contants';
import { FilesConfigurations } from '../../../../model/filesConfigurations';

@Component({
  selector: 'app-file-configuration',
  templateUrl: './file-configuration.component.html',
  standalone: false
})
export class FileConfigurationComponent {
  @Input() fileConfiguration?: FilesConfigurations;
  protected FileFormats = FileFormat.Formats;
  protected LineDelimiters = FileLineDelimiter.FileLineDelimiters;
  protected ColumnDelimiters = FileColumnDelimiter.FileColumnDelimiters;
  protected DecimalSymbols = FileDecimalSymbol.FileDecimalSymbols;
  protected ThousandSeparators = FileThousandSeparator.FileThousandSeparators;
}
