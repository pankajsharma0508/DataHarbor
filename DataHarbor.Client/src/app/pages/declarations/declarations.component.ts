import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ConfigurationService } from '../../../api/configuration.service';
import { DeclarationService } from '../../../api/declaration.service';
import { ProcessService } from '../../../api/process.service';
import { Anchored } from '../../../model/anchored';
import { Declaration } from '../../../model/declaration';
import { ProcessingConfiguration } from '../../../model/processingConfiguration';
import { v4 as uuidv4 } from 'uuid';


@Component({
  selector: 'app-declarations',
  templateUrl: './declarations.component.html',
  styleUrl: './declarations.component.css',
  standalone: false
})
export class DeclarationsComponent implements OnInit {
  showPopup = false;
  protected selectedConfig: ProcessingConfiguration | undefined;
  protected filePath: string = '';
  configurations: Array<ProcessingConfiguration> = [];

  protected requests: Declaration[] = [];
  constructor(private service: DeclarationService,
    private configurationService: ConfigurationService,
    private processService: ProcessService) {
  }

  ngOnInit(): void {
    this.loadConfigurations();
    this.loadDeclarations();
  }

  async loadConfigurations() {
    const configOptions = await lastValueFrom(this.configurationService.apiConfigurationAllGet());
    this.configurations = configOptions;
  }

  async loadDeclarations() {
    const requests = await lastValueFrom(this.service.apiDeclarationAllGet());
    this.requests = requests;
  }

  async announce() {
    const msg = <Anchored>{};
    msg.uniqueId = uuidv4();
    msg.filePath = this.filePath;
    msg.name = this.selectedConfig?.name;
    await lastValueFrom(this.processService.apiProcessSendMessagePost(msg));
    this.showPopup = false;
  }
}
