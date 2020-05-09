import { Component } from '@angular/core';
import { eLayoutType } from '@rocket/ng.core';

@Component({
  selector: 'rocket-layout-account',
  template: `
    <router-outlet></router-outlet>
    <rocket-confirmation></rocket-confirmation>
    <rocket-toast-container right="30px" bottom="30px"></rocket-toast-container>
  `,
})
export class AccountLayoutComponent {
  // required for dynamic component
  static type = eLayoutType.account;
}
