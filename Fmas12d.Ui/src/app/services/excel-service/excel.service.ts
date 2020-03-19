import { Injectable } from '@angular/core';
import * as FileSaver from 'file-saver';
import * as XLSX from 'xlsx';
import * as moment from 'moment';
import { Observable } from 'rxjs';
import { InvoicePaymentFile } from 'src/app/interfaces/InvoicePaymentFile';

const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';

@Injectable({
  providedIn: 'root'
})
export class ExcelService {

  constructor() {
   }

   public exportAsCcgExcelFile(
     json: InvoicePaymentFile[],
     shortCode: string,
     name: string,
     cols: {cell: string, title: string}[]
     ): Observable<any> {

    const createExportFile = new Observable((observer) => {
      try {

        // remove the id column
        json.forEach(row => {
          delete row['id'];
        });

        const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json);
        const fileName = `FMAS12D_ClaimExport_${shortCode}`;

        cols.forEach(col => {
          worksheet[col.cell].v = col.title;
        });

        const workbook: XLSX.WorkBook = { Sheets: { 'claims': worksheet }, SheetNames: ['claims'] };
        const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        this.saveAsExcelFile(excelBuffer, fileName);
        observer.next(`[${shortCode}] ${name}`);
        observer.complete();

      } catch (error) {
        observer.error(`Failed to create export file for ${shortCode}`);
      }
    });

    return createExportFile;
   }

   public exportAsExcelFile(
    json: any[],
    fileName: string,
    cols: {cell: string, title: string}[]
    ): Observable<any> {

   const createExportFile = new Observable((observer) => {
     try {
       const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json);

       cols.forEach(col => {
         worksheet[col.cell].v = col.title;
       });

       let i = 2;
       json.forEach(v => {
       worksheet['B' + (i++).toString()].z = 'dd/mm/yyyy';
       });

       const workbook: XLSX.WorkBook = { Sheets: { 'claims': worksheet }, SheetNames: ['claims'] };
       const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
       this.saveAsExcelFile(excelBuffer, fileName);
       observer.next();
       observer.complete();

     } catch (error) {
       observer.error(`Failed to create export file`);
     }
   });

   return createExportFile;
  }

  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {
      type: EXCEL_TYPE
    });

    FileSaver.saveAs(data, `${fileName}_${moment().format('DDMMYYYY')}${EXCEL_EXTENSION}`);
  }

}
