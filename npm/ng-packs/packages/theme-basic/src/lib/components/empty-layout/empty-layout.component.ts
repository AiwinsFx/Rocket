import { Component } from '@angular/core';
import { eLayoutType } from '@aiwins/ng.core';

@Component({
  selector: 'rocket-layout-empty',
  template: `
    <router-outlet></router-outlet>
    <rocket-confirmation></rocket-confirmation>
    <rocket-toast-container right="30px" bottom="30px"></rocket-toast-container>
  `,
})
export class EmptyLayoutComponent {
  static type = eLayoutType.empty;
}
