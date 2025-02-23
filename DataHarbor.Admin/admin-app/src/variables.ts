import { InjectionToken } from '@angular/core';

export const BASE_PATH = new InjectionToken<string>('basePath', {
    providedIn: 'root',
    factory: () => 'https://localhost:44379',
});
export const COLLECTION_FORMATS = {
    'csv': ',',
    'tsv': '   ',
    'ssv': ' ',
    'pipes': '|'
}
