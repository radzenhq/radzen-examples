import { EventEmitter, Component, Input, Output } from '@angular/core';

@Component({
  selector: 'my-component',
  template: `
    <button (click)="onClick()">{{ text }}</button>
  `
})
export class CustomComponent {
  @Input() text: string;
  @Output() navigate = new EventEmitter();

  onClick() {
    this.navigate.next();
  }
}