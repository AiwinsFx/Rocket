import { CoreModule } from '@aiwins/ng.core';
import { ThemeSharedModule } from '@aiwins/ng.theme.shared';
import { NgModule } from '@angular/core';
import { SettingManagementRoutingModule } from './setting-management-routing.module';
import { SettingManagementComponent } from './components/setting-management.component';
import { NgxsModule } from '@ngxs/store';
import { SettingManagementState } from './states/setting-management.state';

@NgModule({
  declarations: [SettingManagementComponent],
  imports: [
    SettingManagementRoutingModule,
    CoreModule,
    ThemeSharedModule,
    NgxsModule.forFeature([SettingManagementState]),
  ],
})
export class SettingManagementModule {}
