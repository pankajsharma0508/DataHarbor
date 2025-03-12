import { InjectionToken } from '@angular/core';

export const BASE_PATH = new InjectionToken<string>('basePath', {
    providedIn: 'root',
    factory: () => 'http://localhost:5237',
});
export const COLLECTION_FORMATS = {
    'csv': ',',
    'tsv': '   ',
    'ssv': ' ',
    'pipes': '|'
}
