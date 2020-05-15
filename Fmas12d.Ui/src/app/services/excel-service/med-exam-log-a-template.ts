import * as Style from './excel.style';
import { MhaTemplate } from 'src/app/interfaces/mha-template';
import { Fill } from 'exceljs';

export const MED_EXAM_LOG_A_FORMATS: {column: string, format?: string, dateFormat?: string, width?: number }[] = [
  {
    column: 'A',
    dateFormat: 'dd/mm/yyyy',
    width: 12
  },
  {
    column: 'B',
    dateFormat: 'dd/mm/yyyy',
    width: 18
  },
  {
    column: 'C',
    width: 12
  },
  {
    column: 'D',
    width: 24
  },
  {
    column: 'E',
    width: 12
  },
  {
    column: 'F',
    dateFormat: 'dd/mm/yyyy',
    width: 12
  },
  {
    column: 'G',
    width: 18
  },
  {
    column: 'H',
    width: 18
  },
  {
    column: 'I',
    format: '0.00'
  },
  {
    column: 'J',
    format: '0.00'
  },
  {
    column: 'K',
    format: '0.00',
    width: 15
  },
  {
    column: 'L',
    width: 18
  },
  {
    column: 'N',
    width: 24
  },
  {
    column: 'O',
    width: 18
  },
];

export const MED_EXAM_LOG_A_TEMPLATE: MhaTemplate[] = [
  {
    rc: 'A1',
    value: 'Date Logged',
    fill: Style.FILL_GREEN,
    borders: Style.BORDER_ALL_THIN,
    merge: 'A1:A2'
  },
  {
    rc: 'B1',
    value: 'Last Action Date',
    borders: Style.BORDER_ALL_THIN,
    merge: 'B1:B2'
  },
  {
    rc: 'C1',
    value: 'CCG Code',
    fill: Style.FILL_GREEN,
    borders: Style.BORDER_ALL_THIN,
    merge: 'C1:C2'
  },
  {
    rc: 'D1',
    value: 'Doctor Name',
    fill: Style.FILL_YELLOW,
    borders: Style.BORDER_ALL_THIN,
    merge: 'D1:D2'
  },
  {
    rc: 'E1',
    value: 'VSR Number',
    fill: Style.FILL_GREEN,
    borders: Style.BORDER_ALL_THIN,
    merge: 'E1:E2'
  },
  {
    rc: 'F1',
    value: 'Date of Exam',
    fill: Style.FILL_GREEN,
    borders: Style.BORDER_ALL_THIN,
    merge: 'F1:F2'
  },
  {
    rc: 'G1',
    value: 'Patient Identifier',
    fill: Style.FILL_GREEN,
    borders: Style.BORDER_ALL_THIN,
    merge: 'G1:G2'
  },
  {
    rc: 'H1',
    value: 'Date Received',
    fill: Style.FILL_GREEN,
    borders: Style.BORDER_ALL_THIN,
    merge: 'H1:H2'
  },
  {
    rc: 'I1',
    value: 'Value',
    fill: Style.FILL_ORANGE,
    borders: Style.BORDER_ALL_THIN,
    merge: 'I1:I2'
  },
  {
    rc: 'J1',
    value: 'Mileage',
    fill: Style.FILL_GREEN,
    borders: Style.BORDER_ALL_THIN,
    merge: 'J1:J2'
  },
  {
    rc: 'K1',
    value: 'Total Amount',
    fill: Style.FILL_ORANGE,
    borders: Style.BORDER_ALL_THIN,
    merge: 'K1:K2',
    format: '#0.00'
  },
  {
    rc: 'L1',
    value: 'Logged By',
    fill: Style.FILL_GREEN,
    borders: Style.BORDER_ALL_THIN,
    merge: 'L1:L2'
  },
  {
    rc: 'M1',
    value: 'Status',
    borders: Style.BORDER_ALL_THIN,
    merge: 'M1:M2'
  },
  {
    rc: 'N1',
    value: 'IPF Transaction Description',
    fill: Style.FILL_ORANGE,
    borders: Style.BORDER_ALL_THIN,
    merge: 'N1:N2'
  },
  {
    rc: 'O1',
    value: 'Invoice Number',
    fill: Style.FILL_ORANGE,
    borders: Style.BORDER_ALL_THIN,
    merge: 'O1:O2'
  },
  {
    rc: 'P1',
    value: 'Pay Ref',
    borders: Style.BORDER_ALL_THIN,
    merge: 'P1:P2'
  },
  {
    rc: 'Q1',
    value: 'IPF File',
    borders: Style.BORDER_ALL_THIN,
    merge: 'Q1:Q2'
  },
  {
    rc: 'R1',
    value: 'Notes',
    borders: Style.BORDER_ALL_THIN,
    merge: 'R1:R2'
  }
];
