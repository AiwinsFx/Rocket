import { Injectable, Injector } from '@angular/core';
import { addRocketRoutes, eLayoutType, PatchRouteByName, ROCKET } from '@rocket/ng.core';
import { getSettingTabs } from '@rocket/ng.theme.shared';
import { Store } from '@ngxs/store';
import { eSettingManagementRouteNames } from '@rocket/ng.setting-management';

@Injectable({
  providedIn: 'root',
})
export class SettingManagementConfigService {
  get store(): Store {
    return this.injector.get(Store);
  }

  constructor(private injector: Injector) {
    const route = {
      name: eSettingManagementRouteNames.Settings,
      path: 'setting-management',
      parentName: 'RocketUiNavigation::Menu:Administration',
      requiredPolicy: 'RocketAccount.SettingManagement',
      layout: eLayoutType.application,
      order: 6,
      iconClass: 'fa fa-cog',
    } as ROCKET.FullRoute;

    addRocketRoutes(route);

    setTimeout(() => {
      const tabs = getSettingTabs();
      if (!tabs || !tabs.length) {
        this.store.dispatch(
          new PatchRouteByName('RocketSettingManagement::Settings', { ...route, invisible: true }),
        );
      }
    });
  }
}
