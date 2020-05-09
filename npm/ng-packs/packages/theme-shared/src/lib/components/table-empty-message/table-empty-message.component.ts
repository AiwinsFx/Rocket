import { Component, OnInit, Input } from '@angular/core';

@Component({
  // tslint:disable-next-line: component-selector
  selector: '[rocket-table-empty-message]',
  template: `
    <td class="text-center" [attr.colspan]="colspan">
      {{ emptyMessage | rocketLocalization }}
    </td>
  `
})
export class TableEmptyMessageComponent {
  @Input()
  colspan = 2;

  @Input()
  message: string;

  @Input()
  localizationResource = 'RocketAccount';

  @Input()
  localizationProp = 'NoDataAvailableInDatatable';

  get emptyMessage(): string {
    return this.message || `${this.localizationResource}::${this.localizationProp}`;
  }
}
