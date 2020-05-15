import * as Style from './excel.style';
import { MhaTemplate } from 'src/app/interfaces/mha-template';
import { Fill } from 'exceljs';

export const MHA_BATCH_COLUMN_COLOURS: {column: string, fill: Fill}[] = [
  {
    column: 'B',
    fill: Style.FILL_GREEN
  },
  {
    column: 'C',
    fill: null
  },
  {
    column: 'D',
    fill: Style.FILL_GREEN
  },
  {
    column: 'E',
    fill: Style.FILL_GREEN
  },
  {
    column: 'F',
    fill: Style.FILL_GREEN
  },
  {
    column: 'G',
    fill: Style.FILL_BLUE
  },
  {
    column: 'H',
    fill: Style.FILL_BLUE
  },
  {
    column: 'I',
    fill: Style.FILL_GREEN
  },
  {
    column: 'J',
    fill: Style.FILL_GREEN
  },
  {
    column: 'K',
    fill: Style.FILL_GREEN
  },
  {
    column: 'L',
    fill: Style.FILL_GREEN
  },
  {
    column: 'M',
    fill: Style.FILL_GREEN
  },
  {
    column: 'N',
    fill: Style.FILL_GREEN
  },
  {
    column: 'O',
    fill: Style.FILL_BLUE
  },
  {
    column: 'P',
    fill: Style.FILL_GREEN
  },
  {
    column: 'Q',
    fill: Style.FILL_GREEN
  },
  {
    column: 'R',
    fill: Style.FILL_BLUE
  },
  {
    column: 'S',
    fill: Style.FILL_GREY
  }
]

export const MHA_BATCH_COLUMN_FORMAT: {column: string, width?: number, format?: string, dateFormat?: string}[] = [
  {
    column: 'B',
    width: 22
  },
  {
    column: 'C',
    width: 12
  },
  {
    column: 'D',
    width: 16
  },
  {
    column: 'E',
    width: 16,
    dateFormat: 'dd/mm/yyyy'
  },
  {
    column: 'F',
    width: 16,
    dateFormat: 'dd/mm/yyyy'
  },
  {
    column: 'G',
    width: 16
  },
  {
    column: 'H',
    width: 16
  },
  {
    column: 'I',
    width: 12
  },
  {
    column: 'J',
    width: 12
  },
  {
    column: 'K',
    width: 12
  },
  {
    column: 'L',
    width: 12
  },
  {
    column: 'M',
    width: 12
  },
  {
    column: 'N',
    width: 32
  },
  {
    column: 'O',
    width: 12
  },
  {
    column: 'P',
    width: 12,
    format: '0.00'
  },
  {
    column: 'Q',
    width: 12
  },
  {
    column: 'R',
    width: 12
  },
  {
    column: 'S',
    width: 12
  }

]

export const MHA_BATCH_TEMPLATE: MhaTemplate[] = [
  {
    rc: 'B2',
    value: '',
    fill: Style.FILL_GREEN,
    font: Style.FONT_EXTRA_LARGE,
    borders: Style.BORDER_MD_MD_N_MD,
    merge: 'B2:D4'
  },
  {
    rc: 'B5',
    value: 'Enter Trust-Code Above',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_TN_MD_MD_MD,
    merge: 'B5:D5'
  },
  {
    rc: 'G2',
    value: 'Invoice Payments File',
    fill: Style.FILL_GREY,
    font: Style.FONT_LARGE,
    borders: Style.BORDER_MD_MD_N_MD,
    merge: 'G2:L4'
  },
  {
    rc: 'G5',
    value: 'FMAS12D File Generator',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_N_MD_MD_MD,
    merge: 'G5:L5'
  },
  {
    rc: 'B7',
    value: '',
    borders: Style.BORDER_ALL,
    merge: 'B7:F15'
  },
  {
    rc: 'H7',
    value: '',
    borders: Style.BORDER_ALL,
    merge: 'H7:L15'
  },
  {
    rc: 'M7',
    value: '',
    borders: Style.BORDER_ALL,
    merge: 'M7:P12'
  },
  {
    rc: 'M13',
    value: '',
    borders: Style.BORDER_ALL,
    merge: 'M13:P15'
  },
  {
    rc: 'B17',
    value: 'Transaction Data',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL,
    merge: 'B17:H17'
  },
  {
    rc: 'I17',
    value: 'Account Code',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL,
    merge: 'I17:M17'
  },
  {
    rc: 'B18',
    value: 'Transaction Description',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'C18',
    value: 'Vendor Code',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'D18',
    value: 'Invoice Number',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'E18',
    value: 'Invoice Date',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'F18',
    value: 'Invoice Received Date',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'G18',
    value: 'Payment Terms',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'H18',
    value: 'Transaction Type',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'I18',
    value: 'Cost Centre',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'J18',
    value: 'Subjective',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'K18',
    value: 'Analysis 1',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'L18',
    value: 'Analysis 2',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'M18',
    value: 'Analysis 3',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL
  },
  {
    rc: 'N17',
    value: 'Item Description (Max 240 char)',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL,
    merge: 'N17:N18'
  },
  {
    rc: 'O17',
    value: 'Item Type',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL,
    merge: 'O17:O18'
  },
  {
    rc: 'P17',
    value: 'Line Amount',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL,
    merge: 'P17:P18'
  },
  {
    rc: 'Q17',
    value: 'Unit Amount',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL,
    merge: 'Q17:Q18'
  },
  {
    rc: 'R17',
    value: 'Tax Code',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL,
    merge: 'R17:R18'
  },
  {
    rc: 'S17',
    value: 'Line Valid',
    fill: Style.FILL_GREY,
    borders: Style.BORDER_ALL,
    merge: 'S17:S18'
  }
];
