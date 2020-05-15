import { CoreModule } from '@aiwins/ng.core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ThemeBasicModule } from '@aiwins/ng.theme.basic';
import { ThemeSharedModule } from '@aiwins/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';

@NgModule({
  declarations: [],
  imports: [
    CoreModule,
    ThemeSharedModule,
    ThemeBasicModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
  ],
  exports: [
    CoreModule,
    ThemeSharedModule,
    ThemeBasicModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
  ],
  providers: [],
})
export class SharedModule {}
