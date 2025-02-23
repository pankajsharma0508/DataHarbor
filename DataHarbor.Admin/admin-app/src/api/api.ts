export * from './configuration.service';
import { ConfigurationService } from './configuration.service';
export * from './processRequest.service';
import { ProcessRequestService } from './processRequest.service';
export const APIS = [ConfigurationService, ProcessRequestService];
