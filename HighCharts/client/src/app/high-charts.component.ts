import {Input, ElementRef, Component} from '@angular/core';
import {map, get} from 'lodash';

import * as Highcharts from 'highcharts';

@Component({
  selector: 'high-charts',
  template: '&nbsp;'
})
export class HighChartsComponent {
  @Input() type: string;
  @Input() category: string;
  @Input() value: string;
  @Input() data: any[];

  constructor(private element: ElementRef) {}

  ngOnInit() {}

  ngOnChanges() {
    if (this.data && this.value && this.category) {
      const data = this.data.map(item => {
        const name = get(item, this.category);
        const value = parseFloat(get(item, this.value));

        return {
          name: name,
          y: value
        };
      });

      Highcharts.chart(this.element.nativeElement, {
        chart: {
          type: this.type
        },
        xAxis: {
          type: 'category'
        },
        legend: {
          enabled: false
        },
        series: [
          {
            colorByPoint: true,
            data: data
          }
        ]
      });
    }
  }
}
