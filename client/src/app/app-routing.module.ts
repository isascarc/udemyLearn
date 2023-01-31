import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {  BarChartComponent } from './bar-chart/trends.component';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { HomeComponent } from './home/home.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsaveChangesGuard } from './_guards/prevent-unsave-changes.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'members', component: MemberListComponent },
      { path: 'member/edit', component: MemberEditComponent, canDeactivate:[PreventUnsaveChangesGuard] },
      { path: 'members/:username', component: MemberDetailComponent },
      { path: 'trends', component: BarChartComponent },
    ]
  },
  { path: 'errors', component: TestErrorComponent },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
