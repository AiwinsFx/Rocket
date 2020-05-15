import { Injectable } from '@angular/core';
import { addRocketRoutes, eLayoutType } from '@aiwins/ng.core';
import { eTenantManagementRouteNames } from '@aiwins/ng.tenant-management';
@Injectable({
  providedIn: 'root',
})
export class TenantManagementConfigService {
  constructor() {
    addRocketRoutes([
      {
        name: eTenantManagementRouteNames.Administration,
        path: '',
        order: 1,
        wrapper: true,
        iconClass: 'fa fa-wrench',
      },
      {
        name: eTenantManagementRouteNames.TenantManagement,
        path: 'tenant-management',
        parentName: eTenantManagementRouteNames.Administration,
        layout: eLayoutType.application,
        iconClass: 'fa fa-users',
        children: [
          {
            path: 'tenants',
            name: eTenantManagementRouteNames.Tenants,
            order: 1,
            requiredPolicy: 'RocketTenantManagement.Tenants',
          },
        ],
      },
    ]);
  }
}
