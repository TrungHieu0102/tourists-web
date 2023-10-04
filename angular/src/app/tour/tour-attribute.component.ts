import { ConfirmationService } from 'primeng/api';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TourAttributeInListDto, TourAttributesService } from '@proxy/tour-attributes';
import { TourDto, ToursService } from '@proxy/tours';
import { TourAttributeValueDto } from '@proxy/tours/attributes';
import { AttributeType } from '@proxy/trung-hieu-tourists/tour-attributes';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { NotificationService } from '../shared/services/notification.service';
import { UtilityService } from '../shared/services/utility.service';
@Component({
  selector: 'app-tour-attribute',
  templateUrl: './tour-attribute.component.html',
})
export class TourAttributeComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel: boolean = false;
  btnDisabled = false;
  public form: FormGroup;

  attributes: any[] = [];
  fullAttributes: any[] = [];
  tourAttributes: any[] = [];
  showDateTimeControl: boolean = false;
  showDecimalControl: boolean = false;
  showIntControl: boolean = false;
  showVarcharControl: boolean = false;
  showTextControl: boolean = false;

  constructor(
    private tourAttributeService: TourAttributesService,
    private tourService: ToursService,
    private fb: FormBuilder,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef,
    private notificationSerivce: NotificationService,
    private confirmationService: ConfirmationService
  ) {}
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
  ngOnInit(): void {
    this.buildForm();
    this.initFormData();
  }
  initFormData() {
    //Load data to form
    var attributes = this.tourAttributeService.getListAll();
    this.toggleBlockUI(true);
    forkJoin({
      attributes,
    })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: any) => {
          //Push data to dropdown
          this.fullAttributes = response.attributes;
          var attributes = response.attributes as TourAttributeInListDto[];
          attributes.forEach(element => {
            this.attributes.push({
              value: element.id,
              label: element.label,
            });
          });
          this.loadFormDetails(this.config.data?.id);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }
  loadFormDetails(id: string) {
    this.toggleBlockUI(true);
    this.tourService
      .getListTourAttributeAll(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: TourAttributeValueDto[]) => {
          this.tourAttributes = response;
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  saveChange() {
    this.toggleBlockUI(true);
    this.tourService
      .addTourAttribute(this.form.value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.toggleBlockUI(false);
          this.loadFormDetails(this.config.data.id);
        },
        error: err => {
          this.notificationSerivce.showError(err.error.error.message);
          this.toggleBlockUI(false);
        },
      });
  }
  private buildForm() {
    this.form = this.fb.group({
      tourId: new FormControl(this.config.data.id),
      attributeId: new FormControl(null, Validators.required),
      dateTimeValue: new FormControl(null),
      decimalValue: new FormControl(null),
      intValue: new FormControl(null),
      varcharValue: new FormControl(null),
      textValue: new FormControl(null),
    });
  }
  removeItem(attribute: TourAttributeValueDto) {
    var id = '';
    if (attribute.dataType == AttributeType.Date) {
      id = attribute.dateTimeId;
    } else if (attribute.dataType == AttributeType.Decimal) {
      id = attribute.decimalId;
    } else if (attribute.dataType == AttributeType.Int) {
      id = attribute.intId;
    } else if (attribute.dataType == AttributeType.Text) {
      id = attribute.textId;
    } else if (attribute.dataType == AttributeType.Varchar) {
      id = attribute.varcharId;
    }
    this.confirmationService.confirm({
      message: 'Bạn có chắc muốn xóa bản ghi này?',
      accept: () => {
        this.deleteItemsConfirmed(attribute, id);
      },
    });
  }
  selectAttribute(event: any) {
    var dataType = this.fullAttributes.filter(x => x.id == event.value)[0].dataType;
    this.showDateTimeControl = false;
    this.showDecimalControl = false;
    this.showIntControl = false;
    this.showTextControl = false;
    this.showVarcharControl = false;
    if (dataType == AttributeType.Date) {
      this.showDateTimeControl = true;
    } else if (dataType == AttributeType.Decimal) {
      this.showDecimalControl = true;
    } else if (dataType == AttributeType.Int) {
      this.showIntControl = true;
    } else if (dataType == AttributeType.Text) {
      this.showTextControl = true;
    } else if (dataType == AttributeType.Varchar) {
      this.showVarcharControl = true;
    }
  }
  deleteItemsConfirmed(attribute: TourAttributeValueDto, id: string) {
    this.toggleBlockUI(true);
    this.tourService
      .removeTourAttribute(attribute.attributeId, id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.notificationSerivce.showSuccess('Xóa thành công');
          this.loadFormDetails(this.config.data?.id);
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }
  getDataTypeName(value: number) {
    return AttributeType[value];
  }
  getValueByType(attribute: TourAttributeValueDto, value: number) {
    if (attribute.dataType == AttributeType.Date) {
      return attribute.dateTimeValue;
    } else if (attribute.dataType == AttributeType.Decimal) {
      return attribute.decimalValue;
    } else if (attribute.dataType == AttributeType.Int) {
      return attribute.intValue;
    } else if (attribute.dataType == AttributeType.Text) {
      return attribute.textValue;
    } else if (attribute.dataType == AttributeType.Varchar) {
      return attribute.varcharValue;
    }
  }
  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
      this.btnDisabled = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
        this.btnDisabled = false;
      }, 1000);
    }
  }
}