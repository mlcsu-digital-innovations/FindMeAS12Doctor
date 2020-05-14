import { Border, Borders, Fill, Alignment, Font, Cell } from 'exceljs';

// fonts
export const FONT_EXTRA_LARGE: Partial<Font> = { name: 'Calibri', family: 4, size: 28, bold: true };
export const FONT_STANDARD: Partial<Font> = { name: 'Calibri', size: 11, bold: false };
export const FONT_LARGE: Partial<Font> = { name: 'Calibri', size: 14, bold: true };

// borders
export const BORDER_THIN: Partial<Border> = { style: 'thin' };
export const BORDER_MEDIUM: Partial<Border> = { style: 'medium' };
export const BORDER_THICK: Partial<Border> = { style: 'thick' };

// fills
export const FILL_GREEN: Fill = {
  type: 'pattern',
  pattern: 'solid',
  fgColor: { argb: 'FFCCFFCC'}, bgColor: { argb: 'FFCCFFCC'}
};
export const FILL_BLUE: Fill = {
  type: 'pattern',
  pattern: 'solid',
  fgColor: { argb: 'FFCCFFFF'}, bgColor: { argb: 'FFCCFFFF'}
};
export const FILL_GREY: Fill = {
  type: 'pattern',
  pattern: 'solid',
  fgColor: { argb: 'FFC0C0C0'}, bgColor: { argb: 'FFC0C0C0'}
};
export const FILL_YELLOW: Fill = {
  type: 'pattern',
  pattern: 'solid',
  fgColor: { argb: 'FFFFFF99'}, bgColor: { argb: 'FFFFFF99'}
};
export const FILL_ORANGE: Fill = {
  type: 'pattern',
  pattern: 'solid',
  fgColor: { argb: 'FFFFC000'}, bgColor: { argb: 'FFFFC000'}
};

// ALIGNMENT
export const  ALIGNMENT_CENTRE: Partial<Alignment> = {horizontal: 'center', vertical: 'middle'};

// borders
export const BORDER_MD_MD_TN_MD: Partial<Borders> = {
    top: BORDER_MEDIUM,
    left: BORDER_MEDIUM,
    right: BORDER_MEDIUM,
    bottom: BORDER_THIN
  };

export const BORDER_TN_MD_MD_MD: Partial<Borders> = {
  top: BORDER_THIN,
  left: BORDER_MEDIUM,
  right: BORDER_MEDIUM,
  bottom: BORDER_MEDIUM
};

export const BORDER_N_MD_MD_MD: Partial<Borders> = {
  left: BORDER_MEDIUM,
  right: BORDER_MEDIUM,
  bottom: BORDER_MEDIUM
};

export const BORDER_MD_MD_N_MD: Partial<Borders> = {
  top: BORDER_MEDIUM,
  left: BORDER_MEDIUM,
  right: BORDER_MEDIUM
};

export const BORDER_ALL: Partial<Borders> = {
  top: BORDER_MEDIUM,
  left: BORDER_MEDIUM,
  right: BORDER_MEDIUM,
  bottom: BORDER_MEDIUM
};

export const BORDER_ALL_THIN: Partial<Borders> = {
  top: BORDER_THIN,
  left: BORDER_THIN,
  right: BORDER_THIN,
  bottom: BORDER_THIN
};
