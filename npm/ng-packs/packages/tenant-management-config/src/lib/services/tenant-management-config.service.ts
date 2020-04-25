import { Injectable } from '@angular/core';
import { addRocketRoutes, eLayoutType } from '@rocket/ng.core';

@Injectable({
  providedIn: 'root',
})
export class TenantManagementConfigService {
  constructor() {
    addRocketRoutes({
      name: 'RocketTenantManagement::Menu:TenantManagement',
      path: 'tenant-management',
      parentName: 'RocketUiNavigation::Menu:Administration',
      layout: eLayoutType.application,
      iconClass: 'fa fa-users',
      children: [
        {
          path: 'tenants',
          name: 'RocketTenantManagement::Tenants',
          order: 1,
          requiredPolicy: 'RocketTenantManagement.Tenants',
        },
      ],
    });
  }
}
