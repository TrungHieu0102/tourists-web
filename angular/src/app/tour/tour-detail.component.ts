import { PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TourCategoriesService, TourCategoryInListDto } from '@proxy/tour-categories';
import { TourDto, TourInListDto, ToursService } from '@proxy/tours';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-tour-detail',
  templateUrl: './tour-detail.component.html',
})
export class TourDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;

  public form: FormGroup;

  //Dropdown
  tourCategories: any[] = [];
  countries: any[] =[];
  tourTypes: any[] = [];
  selectedEntity = {} as TourDto;

  constructor(
    private tourService:ToursService,
    private tourCategoryService: TourCategoriesService,
    private fb: FormBuilder
  ) {}

  ngOnDestroy(): void {}

  ngOnInit(): void {
    this.buildForm();
  }

  loadFormDetails(id: string) {
    this.toggleBlockUI(true);
    this.tourService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: TourDto) => {
          this.selectedEntity = response;
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }
  saveChange(){


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

  private buildForm() {
    this.form = this.fb.group({
      name: new FormControl(this.selectedEntity.name || null, Validators.required),
      code: new FormControl(this.selectedEntity.code || null, Validators.required),
      slug: new FormControl(this.selectedEntity.slug || null, Validators.required),
      sku: new FormControl(this.selectedEntity.sku || null, Validators.required),
      countryId: new FormControl(this.selectedEntity.countryId || null, Validators.required),
      categoryId: new FormControl(this.selectedEntity.categoryId || null, Validators.required),
      tourType: new FormControl(this.selectedEntity.tourType || null, Validators.required),
      sortOrder: new FormControl(this.selectedEntity.sortOrder || null, Validators.required),
      sellPrice: new FormControl(this.selectedEntity.sellPrice || null, Validators.required),
      visibility: new FormControl(this.selectedEntity.visibility || false),
      isActive: new FormControl(this.selectedEntity.isActive || false),
      seoMetaDescription: new FormControl(this.selectedEntity.seoMetaDescription || null),
      description: new FormControl(this.selectedEntity.description || null),
    });
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