/**
 * DataHarbor.WebAPI
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { Attachment } from './attachment';
import { FilesConfigurations } from './filesConfigurations';
import { LayoutMapping } from './layoutMapping';

export interface ProcessingConfiguration { 
    id?: string;
    uniqueId?: string;
    name?: string;
    description?: string;
    modifiedOn?: Date;
    mailboxFileName?: string;
    mailboxFilePath?: string;
    operatorFilesConfigurations?: FilesConfigurations;
    layoutMappings?: Array<LayoutMapping>;
    attachments?: Array<Attachment>;
}