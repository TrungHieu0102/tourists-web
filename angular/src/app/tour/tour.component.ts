import { AuthService, PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { TourCategoriesService, TourCategoryInListDto } from '@proxy/tour-categories';
import { TourDto,TourInListDto, ToursService } from '@proxy/tours';
import { Subject, takeUntil } from 'rxjs';
import { NotificationService } from '../shared/services/notification.service';
import { TourDetailComponent } from './tour-detail.component';
import { DialogService } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-tour',
  templateUrl: './tour.component.html',
  styleUrls: ['./tour.component.scss'],
})
export class TourComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  items: TourInListDto[] = [];
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
    private notificationService: NotificationService
  ) {}
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.tourService
      .getListFilter({
        keyword: '',
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
          name: element.name,
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
      header: 'Thêm mới tour',
      width: '70%',
    });

    ref.onClose.subscribe((data:TourDto) => {
      if (data) {
        this.loadData();
        this.notificationService.showSuccess("Thêm tour thành công");
      }
    });
  }
  private toggleBlockUI(enabled: boolean){
    if(enabled == true){
      this.blockedPanel = true;
    }else{
      setTimeout(()=>{
        this.blockedPanel = false;
      },500);
    }
  }
}
