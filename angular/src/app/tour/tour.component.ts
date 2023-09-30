import { AuthService, PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { TourInListDto, ToursService } from '@proxy/tours';
import { Subject, takeUntil } from 'rxjs';

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

  constructor(private toursService: ToursService) {}
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
}
ngOnInit(): void {
  this.loadData();
}

loadData() {
  this.toursService
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
      },
      error: () => {},
    });
   
}
pageChanged(event: any): void {
  this.skipCount = (event.page -1) * this.maxResultCount;
  this.maxResultCount = event.rows;
  this.loadData();
}
}
