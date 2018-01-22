import { HostListener, Component, Input } from '@angular/core';
import { NavigationEnd, Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';


@Component({
  selector: 'page-title',
  template: '{{ (route.data | async).title }}'
})
export class PageTitleComponent {
  constructor(public route: ActivatedRoute) {
  }
}

@Component({
  selector: 'navigation-menu',
  template: `
  <ul class="ultima-menu ultima-main-menu clearfix">
      <li routerLinkActive="active-menuitem" *ngFor="let page of pages">
        <a [routerLink]="page.link">{{ page.name }}</a>
      </li>
  </ul>`
})
export class NavigationMenuComponent {
  @Input() pages = []
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  menuActive = true;
  mobileMenuActive = false;
  popup = {
    visible: false,
    width: null,
    height: null,
    title: null
  };

  pages = [
      {
        name: 'Orders',
        link: '/orders'
      },
      {
        name: 'Order Details',
        link: '/order-details'
      },
      {
        name: 'Products',
        link: '/products'
      },
  ];

  accessiblePages: any[];

  constructor (public router: Router, private route: ActivatedRoute, private location: Location) {
    router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        const route = this.route.children.find(route => route.outlet == 'popup');
        this.accessiblePages = this.pages;

        if (route) {
          this.popup.width = route.snapshot.queryParams.width || 600;
          this.popup.height = route.snapshot.queryParams.height || null;
          this.popup.title = route.snapshot.data.title;
          setTimeout(() => this.popup.visible = true);
        } else {
          this.popup.visible = false;
        }
      }
    });

  }

  onToggleMenuClick(event) {
    event.preventDefault();

    if(this.isDesktop()) {
      this.menuActive = !this.menuActive;
    } else {
      this.mobileMenuActive = !this.mobileMenuActive;
    }
  }

  onPopupHide() {
    const popup = this.route.children.find(route => route.outlet == 'popup');

    if (popup) {
      this.router.navigate([ { outlets: { popup: null } } ]);
    }
  }

  isDesktop() {
    return window.innerWidth > 1024;
  }
}
