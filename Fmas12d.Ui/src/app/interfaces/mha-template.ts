import { Workbook, Worksheet, Border, Borders, Fill, Alignment, Font } from 'exceljs';

export interface MhaTemplate {
  rc: string;
  value: string;
  font?: Partial<Font>;
  fill?: Fill;
  borders?: Partial<Borders>;
  alignment?: Partial<Alignment>;
  merge?: string;
  format?: string;
}
