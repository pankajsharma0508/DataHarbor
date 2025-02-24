import { Injectable } from '@angular/core';
import { ConfigurationService } from '../api/configuration.service';
import { lastValueFrom } from 'rxjs';
import { ProcessingConfiguration } from '../model/processingConfiguration';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationStore {

  constructor(private service: ConfigurationService) { }

  getAll(): Promise<ProcessingConfiguration[]> {
    return lastValueFrom(this.service.apiConfigurationConfigurationAllGet());
  }

  post(configuration: ProcessingConfiguration) {
    return lastValueFrom(this.service.apiConfigurationPost(configuration));
  }

  get(id: string) {
    return lastValueFrom(this.service.apiConfigurationConfigurationUniqueIdGet(id));
  }

  put(configuration: ProcessingConfiguration) {
    return lastValueFrom(this.service.apiConfigurationPut(configuration));
  }
}
