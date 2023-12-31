import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
  selector: 'app-menu',
  templateUrl: './app.menu.component.html',
})
export class AppMenuComponent implements OnInit {
  model: any[] = [];

  constructor(public layoutService: LayoutService) {}

  ngOnInit() {
    this.model = [
      {
        label: 'Trang chủ',
        items: [{ label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/'] }],
      },
      {
        label: 'Tour',
        items: [
          { label: 'Danh sách tour', icon: 'pi pi-fw pi-circle', routerLink: ['/catalog/tour'] },
          { label: 'Danh sách thuộc tính', icon: 'pi pi-fw pi-circle', routerLink: ['/catalog/attribute'] },
        ],
      },
      {
        label: 'Hệ thống',
        items: [
          { label: 'Danh sách quyền', icon: 'pi pi-fw pi-circle', routerLink: ['/system/role'] },
          { label: 'Danh sách người dùng', icon: 'pi pi-fw pi-circle', routerLink: ['/system/user'] },

        ],
      }
    ];
  }
}
