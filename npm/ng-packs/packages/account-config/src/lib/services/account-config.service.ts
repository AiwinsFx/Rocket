import { addRocketRoutes, eLayoutType } from '@rocket/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AccountConfigService {
  constructor() {
    addRocketRoutes({
      name: 'RocketAccount::Menu:Account',
      path: 'account',
      invisible: true,
      layout: eLayoutType.application,
      children: [
        { path: 'login', name: 'RocketAccount::Login', order: 1 },
        { path: 'register', name: 'RocketAccount::Register', order: 2 },
        { path: 'manage-profile', name: 'RocketAccount::ManageYourProfile', order: 3 },
      ],
    });
  }
}
