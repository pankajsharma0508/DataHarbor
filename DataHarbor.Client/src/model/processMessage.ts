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
import { ProcessMessageStatus } from './processMessageStatus';

export interface ProcessMessage { 
    declarationId?: string;
    status?: ProcessMessageStatus;
    name?: string;
    filePath?: string;
    recievedOn?: Date;
}