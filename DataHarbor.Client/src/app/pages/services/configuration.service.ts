import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ProcessingConfiguration } from '../../../model/processingConfiguration';
import { ConfigurationService } from '../../../api/api';


@Injectable({
  providedIn: 'root'
})
export class ConfigurationStore {

  constructor(private service: ConfigurationService) { }

  getAll(): Promise<ProcessingConfiguration[]> {
    return lastValueFrom(this.service.apiConfigurationAllGet());
  }

  post(configuration: ProcessingConfiguration) {
    return lastValueFrom(this.service.apiConfigurationCreatePost(configuration));
  }

  get(id: string) {
    return lastValueFrom(this.service.apiConfigurationIdGet(id));
  }

  put(configuration: ProcessingConfiguration) {
    return lastValueFrom(this.service.apiConfigurationUpdatePost(configuration));
  }
}
