import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ProcessingConfiguration } from '../model/processingConfiguration';
import { DeclarationService } from '../api/declaration.service';
import { Declaration } from '../model/declaration';

@Injectable({
  providedIn: 'root'
})
export class DeclarationStore {

  constructor(private service: DeclarationService) { }

    post(request: Declaration) {
      return lastValueFrom(this.service.apiDeclarationPost(request));
    }
  
    get(id: string) {
      return lastValueFrom(this.service.apiDeclarationIdGet(id));
    }
  
    put(request: Declaration) {
      return lastValueFrom(this.service.apiDeclarationPut(request));
    }
}
