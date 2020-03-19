export interface InvoicePaymentFile {
  TransactionDescription: string;
  VendorCode: string;
  InvoiceNumber: string;
  InvoiceDate: Date;
  InvoiceReceivedDate: Date;
  PaymentTerms: string;
  TransactionType: string;
  CostCentre: string;
  Subjective: string;
  Analysis1: string;
  Analysis2: string;
  Analysis3: string;
  ItemDescription: string;
  ItemType: string;
  LineAmount: number;
  UnitAmount: number;
  TaxCode: string;
  LineValid: string;
  id?: number;
}
