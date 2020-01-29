import { ClaimsCreatePage } from './claims-create.page';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EllipsisPipe } from 'src/app/pipes/ellipsis.pipe';

const routes: Routes = [
  {
    path: '',
    component: ClaimsCreatePage
  }
];

@NgModule({
  imports: [
    CommonModule,
    ComponentsModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [ClaimsCreatePage, EllipsisPipe]
})
export class ClaimsCreatePageModule {}
