import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';

import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';

import { TourDetailComponent } from './tour-detail.component';
import { TourType } from '@proxy/trung-hieu-tourists/tours';
import { ConfirmationService } from 'primeng/api';
import { TourAttributeComponent } from './tour-attribute.component';
import { TourCategoriesService, TourCategoryInListDto } from '@proxy/catalog/tour-categories';
import { TourInListDto, ToursService, TourDto } from '@proxy/catalog/tours';
import { NotificationService } from 'src/app/shared/services/notification.service';

@Component({
  selector: 'app-tour',
  templateUrl: './tour.component.html',
  styleUrls: ['./tour.component.scss'],
})
export class TourComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: TourInListDto[] = [];
  public selectedItems:TourInListDto[] = [];

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  //Filter
  tourCategories: any[] = [];
  keyword: string = '';
  categoryId: string = '';

  constructor(
    private tourService: ToursService,
    private tourCategoryService: TourCategoriesService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit(): void {
    this.loadTourCategories();
    this.loadData();
  }

  loadData() {
    this.toggleBlockUI(true);
    this.tourService
      .getListFilter({
        keyword: this.keyword,
        categoryId: this.categoryId,
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<TourInListDto>) => {
          this.items = response.items;
          this.totalCount = response.totalCount;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  loadTourCategories() {
    this.tourCategoryService.getListAll().subscribe((response: TourCategoryInListDto[]) => {
      response.forEach(element => {
        this.tourCategories.push({
          value: element.id,
          label: element.name,
        });
      });
    });
  }

  pageChanged(event: any): void {
    this.skipCount = (event.page - 1) * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }
  showAddModal() {
    const ref = this.dialogService.open(TourDetailComponent, {
      header: 'Thêm mới Tour',
      width: '70%',
    });

    ref.onClose.subscribe((data: TourDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess('Thêm tour thành công');
        this.selectedItems = [];
      }
    });
  }

  showEditModal() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError('Bạn phải chọn một bản ghi');
      return;
    }
    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(TourDetailComponent, {
      data: {
        id: id,
      },
      header: 'Cập nhật tour',
      width: '70%',
    });
    ref.onClose.subscribe((data: TourDto) => {
      if (data) {
        this.loadData();
        this.selectedItems = [];
        this.notificationService.showSuccess('Cập nhật tour thành công');
      }
    });
  }
  deleteItems(){
    if(this.selectedItems.length == 0){
      this.notificationService.showError("Phải chọn ít nhất một bản ghi");
      return;
    }
    var ids =[];
    this.selectedItems.forEach(element=>{
      ids.push(element.id);
    });
    this.confirmationService.confirm({
      message:'Bạn có chắc muốn xóa bản ghi này?',
      accept:()=>{
        this.deleteItemsConfirmed(ids);
      }
    })
  }
  manageTourAttribute(id: string) {
    const ref = this.dialogService.open(TourAttributeComponent, {
      data: {
        id: id,
      },
      header: 'Quản lý thuộc tính tour',
      width: '70%',
    });

    ref.onClose.subscribe((data: TourDto) => {
      if (data) {
        this.loadData();
        this.selectedItems = [];
        this.notificationService.showSuccess('Cập nhật thuộc tính tour thành công');
      }
    });
  }
  deleteItemsConfirmed(ids: string[]){
    this.toggleBlockUI(true);
    this.tourService.deleteMultiple(ids).pipe(takeUntil(this.ngUnsubscribe)).subscribe({
      next: ()=>{
        this.notificationService.showSuccess("Xóa thành công");
        this.loadData();
        this.selectedItems = [];
        this.toggleBlockUI(false);
      },
      error:()=>{
        this.toggleBlockUI(false);
      }
    })
  }
  getTourTypeName(value: number){
    return TourType[value];
  }

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);
    }
  }
}