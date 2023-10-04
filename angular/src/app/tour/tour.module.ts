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
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import { TourDetailComponent } from './tour-detail.component';
import {InputNumberModule} from 'primeng/inputnumber';
import {CheckboxModule} from 'primeng/checkbox';
import {InputTextareaModule} from 'primeng/inputtextarea';
import {EditorModule} from 'primeng/editor';
import { TourSharedModule } from '../shared/modules/tour-shared.module';
import {BadgeModule} from 'primeng/badge';
import {ImageModule} from 'primeng/image';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import { TourAttributeComponent } from './tour-attribute.component';
import { CalendarModule } from 'primeng/calendar';

@NgModule({
  declarations: [TourComponent,TourDetailComponent,TourAttributeComponent],
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
    ProgressSpinnerModule,
    ProgressSpinnerModule,
    DynamicDialogModule,
    InputNumberModule,
    CheckboxModule,
    InputTextareaModule,
    EditorModule,
    TourSharedModule,
    BadgeModule,
    ImageModule,
    ConfirmDialogModule,
    CalendarModule
  ],
})
export class TourModule {}
