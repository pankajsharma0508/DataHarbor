import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { DeclarationService } from '../../../api/declaration.service';
import { Declaration } from '../../../model/declaration';

@Injectable({
  providedIn: 'root'
})
export class DeclarationStore {

  constructor(private service: DeclarationService) { }

    get(id: string) {
      return lastValueFrom(this.service.apiDeclarationIdGet(id));
    }
  
    put(request: Declaration) {
      return lastValueFrom(this.service.apiDeclarationUpdatePut(request));
    }
}
