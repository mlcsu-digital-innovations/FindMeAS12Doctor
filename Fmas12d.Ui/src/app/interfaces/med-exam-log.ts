export interface MedExamLogA {
  dateLogged: Date;
  lastActionDate: Date;
  ccgCode: string;
  doctorName: string;
  vsrNUmber: number;
  dateOfExam: Date;
  patientIdentifer: string;
  dateReceived: Date;
  value: number;
  mileage: number;
  total: number;
  loggedBy: string;
  status: string;
  ipfTransactionDescription: string;
  invoiceNumber: string;
  payRef?: number;
  ipfFile: string;
  notes: string;
}
