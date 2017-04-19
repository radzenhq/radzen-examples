import { HostListener, Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';


@Component({
  selector: 'page-title',
  template: '{{ (route.data | async).title }}'
})
export class PageTitleComponent {
  constructor(private route: ActivatedRoute) {
  }
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  menuActive = true;
  mobileMenuActive = false;
  popupRoute: ActivatedRoute;

  constructor (public router: Router, private route: ActivatedRoute, private location: Location) {
   router.events.subscribe(() => {
      this.popupRoute = this.route.children.find(route => route.outlet == 'popup');
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

  isDesktop() {
    return window.innerWidth > 1024;
  }
}
