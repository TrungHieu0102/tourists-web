import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AttributeComponent } from './attribute/attribute.component';
import { TourComponent } from './tour/tour.component';

const routes: Routes = [
  { path: 'tour', component: TourComponent },
  { path: 'attribute', component: AttributeComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogRoutingModule {}