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

export const LAYOUTS = [ApplicationLayoutComponent, AccountLayoutComponent, EmptyLayoutComponent];

@NgModule({
  declarations: [...LAYOUTS, ValidationErrorComponent],
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
          'RocketAccount::ThisFieldMustBeAStringOrArrayTypeWithAMaximumLengthoOf{0}[{{ requiredLength }}]',
        min: 'RocketAccount::ThisFieldMustBeBetween{0}And{1}[{{ min }},{{ max }}]',
        minlength:
          'RocketAccount::ThisFieldMustBeAStringOrArrayTypeWithAMinimumLengthOf{0}[{{ requiredLength }}]',
        required: 'RocketAccount::ThisFieldIsRequired.',
        passwordMismatch: 'RocketIdentity::Identity.PasswordConfirmationFailed',
      },
      errorTemplate: ValidationErrorComponent,
    }),
  ],
  exports: [...LAYOUTS],
  entryComponents: [...LAYOUTS, ValidationErrorComponent],
})
export class ThemeBasicModule {
  constructor(private initialService: InitialService) {}
}
