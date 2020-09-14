import { AssessmentCreateComponent } from './assessment-create/assessment-create.component';
import { AssessmentEditComponent } from './assessment-edit/assessment-edit.component';
import { AssessmentListComponent } from './assessment-list/assessment-list.component';
import { AssessmentOutcomeComponent } from './assessment-outcome/assessment-outcome.component';
import { AssessmentViewComponent } from './assessment-view/assessment-view.component';
import { Routes } from '@angular/router';

export const AssessmentRoutes: Routes = [
  {
    path: 'assessment',
    pathMatch: 'full',
    redirectTo: 'assessment/list'
  },
  {
    path: 'assessment/edit/:referralId',
    component: AssessmentEditComponent
  },
  {
    path: 'assessment/list',
    component: AssessmentListComponent
  },
  {
    path: 'assessment/outcome/:referralId',
    component: AssessmentOutcomeComponent
  },
  {
    path: 'assessment/view/:referralId',
    component: AssessmentViewComponent
  },
  {
    path: 'assessment/new/:referralId',
    component: AssessmentCreateComponent
  }
];
