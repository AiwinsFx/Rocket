import { CoreModule } from '@aiwins/ng.core';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { NgxsModule } from '@ngxs/store';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { ThemeSharedModule } from '@aiwins/ng.theme.shared';
import { AccountConfigModule } from '@aiwins/ng.account.config';
import { IdentityConfigModule } from '@aiwins/ng.identity.config';
import { TenantManagementConfigModule } from '@aiwins/ng.tenant-management.config';
import { SettingManagementConfigModule } from '@aiwins/ng.setting-management.config';

const LOGGERS = [NgxsLoggerPluginModule.forRoot({ disabled: false })];

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule.forRoot({
      environment,
    }),
    ThemeSharedModule.forRoot(),
    AccountConfigModule.forRoot({ redirectUrl: '/' }),
    IdentityConfigModule,
    TenantManagementConfigModule,
    SettingManagementConfigModule,
    NgxsModule.forRoot(),
    SharedModule,
    ...(environment.production ? [] : LOGGERS),
  ],
  declarations: [AppComponent],
  bootstrap: [AppComponent],
})
export class AppModule {}
