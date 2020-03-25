import { Injectable } from '@angular/core';
import * as FileSaver from 'file-saver';
import * as XLSX from 'xlsx';
import * as moment from 'moment';
import { Observable } from 'rxjs';
import { InvoicePaymentFile } from 'src/app/interfaces/InvoicePaymentFile';

import { Workbook, Worksheet, Border, Borders, Fill, Alignment, Font } from 'exceljs';
import * as Style from './excel.style';

import { MHA_BATCH_TEMPLATE, MHA_BATCH_COLUMN_COLOURS } from './mha-batch-template';
import { MhaTemplate } from 'src/app/interfaces/mha-template';


const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';

@Injectable({
  providedIn: 'root'
})
export class ExcelService {

  constructor() {
   }

   worksheet: Worksheet;

   public createMhaBatchExport(
    json: InvoicePaymentFile[],
    shortCode: string,
    name: string
    ): Observable<any> {
    const createExportFile = new Observable((observer) => {
      try {
        const workbook = new Workbook();
        this.worksheet = workbook.addWorksheet('Sheet1');
        const fileName = `${shortCode} MHA BATCH`;

        // worksheet.getCell('N3').value = 'Mandatory (User Input)';
        // worksheet.getCell('N4').value = 'Mandatory (From LOV)';
        // worksheet.getCell('N5').value = 'Optional';

        MHA_BATCH_TEMPLATE.forEach(cell => {
          this.addCell(cell);
        });

        this.worksheet.getCell('B2').value = shortCode;

        const startDataRow = this.worksheet.rowCount;

        const dataWithoutId = (JSON.parse(JSON.stringify(json)));

        // remove the id column
        dataWithoutId.forEach(row => {
          delete row['id'];
        });

        dataWithoutId.forEach(row => {
          const data = [];
          data.push('');
          for (const key in row) {
            if (row.hasOwnProperty(key)) {
              data.push(row[key]);
            }
          }
          this.worksheet.addRow(data);
        });

        const endDataRow = this.worksheet.rowCount;

        for (let i = startDataRow + 1; i <= endDataRow; i++) {
          MHA_BATCH_COLUMN_COLOURS.forEach(fill => {
            const cell = `${fill.column}${i.toString()}`;
            this.setCellColour(cell, fill.fill);
          });
        }

        workbook.xlsx.writeBuffer()
        .then((data) => {
          const blob = new Blob([data], {type: 'application/vnd.openxmlformats-officedocument.spreadsheet.sheet'});
          this.saveAsExcelFile(blob, fileName);
        });
      } catch (error) {
        console.log(error);
        observer.error(`Failed to create export file for ${shortCode} ${name}`);
      }
    });
    return createExportFile;
  }

  private setCellColour(rc: string, fill: Fill) {
    const cell = this.worksheet.getCell(rc);

    cell.border = Style.BORDER_ALL_THIN;

    if (fill !== null) {
      cell.fill = fill;
    }
  }

  private addCell(cell: MhaTemplate) {

    const cl = this.worksheet.getCell(cell.rc);
    cl.value = cell.value;
    cl.alignment = cell.alignment;
    cl.border = cell.borders;
    cl.fill = cell.fill;
    cl.font = cell.font ? cell.font : Style.FONT_STANDARD;

    if (cell.merge) {
      this.worksheet.mergeCells(cell.merge);
    }
  }


   public exportAsCcgExcelFile(
     json: InvoicePaymentFile[],
     shortCode: string,
     name: string,
     cols: {cell: string, title: string}[]
     ): Observable<any> {

    const createExportFile = new Observable((observer) => {
      try {

        const dataWithoutId = (JSON.parse(JSON.stringify(json)));

        // remove the id column
        dataWithoutId.forEach(row => {
          delete row['id'];
        });

        const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(dataWithoutId);
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
