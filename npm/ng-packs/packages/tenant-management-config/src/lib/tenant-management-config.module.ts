import { NgModule, APP_INITIALIZER } from '@angular/core';
import { TenantManagementConfigService } from './services/tenant-management-config.service';
import { noop } from '@aiwins/ng.core';

@NgModule({
  providers: [{ provide: APP_INITIALIZER, deps: [TenantManagementConfigService], useFactory: noop, multi: true }],
})
export class TenantManagementConfigModule {}
