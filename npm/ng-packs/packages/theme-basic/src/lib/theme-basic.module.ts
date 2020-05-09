import { CoreModule } from '@rocket/ng.core';
import { ThemeSharedModule } from '@rocket/ng.theme.shared';
import { NgModule } from '@angular/core';
import { NgbCollapseModule, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { NgxsModule } from '@ngxs/store';
import { AccountLayoutComponent } from './components/account-layout/account-layout.component';
import { ApplicationLayoutComponent } from './components/application-layout/application-layout.component';
import { EmptyLayoutComponent } from './components/empty-layout/empty-layout.component';
import { LayoutState } from './states/layout.state';
import { ValidationErrorComponent } from './components/validation-error/validation-error.component';
import { InitialService } from './services/initial.service';
import { LogoComponent } from './components/logo/logo.component';
import { RoutesComponent } from './components/routes/routes.component';
import { NavItemsComponent } from './components/nav-items/nav-items.component';

export const LAYOUTS = [ApplicationLayoutComponent, AccountLayoutComponent, EmptyLayoutComponent];

@NgModule({
  declarations: [
    ...LAYOUTS,
    ValidationErrorComponent,
    LogoComponent,
    NavItemsComponent,
    RoutesComponent,
  ],
  imports: [
    CoreModule,
    ThemeSharedModule,
    NgbCollapseModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    NgxsModule.forFeature([LayoutState]),
    NgxValidateCoreModule.forRoot({
      targetSelector: '.form-group',
      blueprints: {
        email: 'RocketAccount::ThisFieldIsNotAValidEmailAddress.',
        max: 'RocketAccount::ThisFieldMustBeBetween{0}And{1}[{{ min }},{{ max }}]',
        maxlength:
          'RocketAccount::ThisFieldMustBeAStringOrArrayTypeWithAMaximumLengthOf{0}[{{ requiredLength }}]',
        min: 'RocketAccount::ThisFieldMustBeBetween{0}And{1}[{{ min }},{{ max }}]',
        minlength:
          'RocketAccount::ThisFieldMustBeAStringOrArrayTypeWithAMinimumLengthOf{0}[{{ requiredLength }}]',
        required: 'RocketAccount::ThisFieldIsRequired.',
        passwordMismatch: 'RocketIdentity::Identity.PasswordConfirmationFailed',
      },
      errorTemplate: ValidationErrorComponent,
    }),
  ],
  exports: [...LAYOUTS, LogoComponent, ValidationErrorComponent],
  entryComponents: [...LAYOUTS, ValidationErrorComponent],
})
export class ThemeBasicModule {
  constructor(private initialService: InitialService) {}
}
