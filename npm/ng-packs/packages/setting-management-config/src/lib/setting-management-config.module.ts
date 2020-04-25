import { NgModule, APP_INITIALIZER } from '@angular/core';
import { SettingManagementConfigService } from './services/setting-management-config.service';
import { noop } from '@rocket/ng.core';

@NgModule({
  providers: [{ provide: APP_INITIALIZER, deps: [SettingManagementConfigService], useFactory: noop, multi: true }],
})
export class SettingManagementConfigModule {}
