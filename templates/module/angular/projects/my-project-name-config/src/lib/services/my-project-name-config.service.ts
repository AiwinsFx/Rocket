import { Injectable } from '@angular/core';
import { eLayoutType, addRocketRoutes, ROCKET } from '@rocket/ng.core';
import { addSettingTab } from '@rocket/ng.theme.shared';
import { MyProjectNameSettingsComponent } from '../components/my-project-name-settings.component';

@Injectable({
  providedIn: 'root',
})
export class MyProjectNameConfigService {
  constructor() {
    addRocketRoutes({
      name: 'MyProjectName',
      path: 'my-project-name',
      layout: eLayoutType.application,
      order: 2,
    } as ROCKET.FullRoute);

    const route = addSettingTab({
      component: MyProjectNameSettingsComponent,
      name: 'MyProjectName Settings',
      order: 1,
      requiredPolicy: '',
    });
  }
}
