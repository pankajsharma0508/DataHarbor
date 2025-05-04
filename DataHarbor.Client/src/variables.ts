import { InjectionToken } from '@angular/core';

export const BASE_PATH = new InjectionToken<string>('basePath', {
    providedIn: 'root',
    factory: () => 'https://dataharbor.local/api',
});
export const COLLECTION_FORMATS = {
    'csv': ',',
    'tsv': '   ',
    'ssv': ' ',
    'pipes': '|'
}
