export class AssessmentOutcomeDoctor {
  attended?: boolean;
  id: number;  

  constructor(attended: boolean, id: number) {
    this.attended = attended;
    this.id = id;
  }
}