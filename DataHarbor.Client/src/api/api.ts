export * from './configuration.service';
import { ConfigurationService } from './configuration.service';
export * from './declaration.service';
import { DeclarationService } from './declaration.service';
export * from './process.service';
import { ProcessService } from './process.service';
export const APIS = [ConfigurationService, DeclarationService, ProcessService];
