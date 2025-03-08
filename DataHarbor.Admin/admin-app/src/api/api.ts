export * from './configuration.service';
import { ConfigurationService } from './configuration.service';
export * from './declaration.service';
import { DeclarationService } from './declaration.service';
export const APIS = [ConfigurationService, DeclarationService];
