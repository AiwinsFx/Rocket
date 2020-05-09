import { addRocketRoutes, eLayoutType } from '@rocket/ng.core';
import { Injectable } from '@angular/core';
import { eAccountRouteNames } from '@rocket/ng.account';

@Injectable({
  providedIn: 'root',
})
export class AccountConfigService {
  constructor() {
    addRocketRoutes({
      name: eAccountRouteNames.Account,
      path: 'account',
      invisible: true,
      layout: eLayoutType.application,
      children: [
        { path: 'login', name: eAccountRouteNames.Login, order: 1 },
        { path: 'register', name: eAccountRouteNames.Register, order: 2 },
        { path: 'manage-profile', name: eAccountRouteNames.ManageProfile, order: 3 },
      ],
    });
  }
}
