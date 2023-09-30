import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { TourRoutingModule } from './tour-routing.module';
import { TourComponent } from './tour.component';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import {DropdownModule} from 'primeng/dropdown';
import {InputTextModule} from 'primeng/inputtext';
import {ProgressSpinnerModule} from 'primeng/progressspinner';

@NgModule({
  declarations: [TourComponent],
  imports: [
    SharedModule,
    TourRoutingModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    BlockUIModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    ProgressSpinnerModule
  ],
})
export class TourModule {}