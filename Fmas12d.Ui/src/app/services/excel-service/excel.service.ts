import { Injectable } from '@angular/core';
import * as FileSaver from 'file-saver';
import * as XLSX from 'xlsx';

const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';

@Injectable({
  providedIn: 'root'
})
export class ExcelService {

  constructor() { }

  public exportAsExcelFile(json: any[], excelFileName: string, cols: {cell: string, title: string}[]): void {

    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json);
    console.log('worksheet', worksheet);

    cols.forEach(col => {
      worksheet[col.cell].v = col.title;
    });

    let i = 2;
    json.forEach(v => {
      // worksheet['C' + (i++).toString()].z = 'dd/mm/yyyy HH:MM';
      worksheet['C' + (i++).toString()].z = 'dd/mm/yyyy';
    });

    const workbook: XLSX.WorkBook = { Sheets: { 'claims': worksheet }, SheetNames: ['claims'] };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    this.saveAsExcelFile(excelBuffer, excelFileName);
  }

  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {
      type: EXCEL_TYPE
    });
    FileSaver.saveAs(data, fileName + '_' + new Date().getTime() + EXCEL_EXTENSION);
  }

}
