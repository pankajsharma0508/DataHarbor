import { Injectable } from '@angular/core';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import { exportDataGrid } from 'devextreme/common/export/excel';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';

@Injectable()
export class ExportService {

    public onExporting(fileName: string | undefined, e: DxDataGridTypes.ExportingEvent) {
        const workbook = new Workbook();
        const worksheet = workbook.addWorksheet(fileName);
        const exportFileName = fileName || 'data';
        exportDataGrid({
            component: e.component,
            worksheet,
            autoFilterEnabled: true,
        }).then(() => {
            if (e.format === 'xlsx')
                workbook.xlsx.writeBuffer().then((buffer) => {
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), `${exportFileName}.xlsx`);
                });
            else if (e.format === 'csv')
                workbook.csv.writeBuffer().then((buffer) => {
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), `${exportFileName}.csv`);
                });
        });
    }
}
