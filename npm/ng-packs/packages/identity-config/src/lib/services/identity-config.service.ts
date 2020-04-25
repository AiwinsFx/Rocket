import { addRocketRoutes, eLayoutType } from '@rocket/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class IdentityConfigService {
  constructor() {
    addRocketRoutes([
      {
        name: 'RocketUiNavigation::Menu:Administration',
        path: '',
        order: 1,
        wrapper: true,
        iconClass: 'fa fa-wrench',
      },
      {
        name: 'RocketIdentity::Menu:IdentityManagement',
        path: 'identity',
        order: 1,
        parentName: 'RocketUiNavigation::Menu:Administration',
        layout: eLayoutType.application,
        iconClass: 'fa fa-id-card-o',
        children: [
          {
            path: 'roles',
            name: 'RocketIdentity::Roles',
            order: 1,
            requiredPolicy: 'RocketIdentity.Roles',
          },
          {
            path: 'users',
            name: 'RocketIdentity::Users',
            order: 2,
            requiredPolicy: 'RocketIdentity.Users',
          },
        ],
      },
    ]);
  }
}
