import { ROCKET, GetAppConfiguration, SessionState, SetTenant } from '@rocket/ng.core';
import { ToasterService } from '@rocket/ng.theme.shared';
import { Component } from '@angular/core';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { finalize, take } from 'rxjs/operators';
import { Account } from '../../models/account';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'rocket-tenant-box',
  templateUrl: './tenant-box.component.html',
})
export class TenantBoxComponent
  implements Account.TenantBoxComponentInputs, Account.TenantBoxComponentOutputs {
  @Select(SessionState.getTenant)
  currentTenant$: Observable<ROCKET.BasicItem>;

  name: string;

  isModalVisible: boolean;

  modalBusy: boolean;

  constructor(
    private store: Store,
    private toasterService: ToasterService,
    private accountService: AccountService,
  ) {}

  onSwitch() {
    const tenant = this.store.selectSnapshot(SessionState.getTenant);
    this.name = (tenant || ({} as ROCKET.BasicItem)).name;
    this.isModalVisible = true;
  }

  save() {
    if (!this.name) {
      this.setTenant(null);
      this.isModalVisible = false;
      return;
    }

    this.modalBusy = true;
    this.accountService
      .findTenant(this.name)
      .pipe(finalize(() => (this.modalBusy = false)))
      .subscribe(({ success, tenantId: id, name }) => {
        if (!success) {
          this.showError();
          return;
        }

        this.setTenant({ id, name });
        this.isModalVisible = false;
      });
  }

  private setTenant(tenant: ROCKET.BasicItem) {
    return this.store.dispatch([new SetTenant(tenant), new GetAppConfiguration()]);
  }

  private showError() {
    this.toasterService.error('RocketUiMultiTenancy::GivenTenantIsNotAvailable', 'RocketUi::Error', {
      messageLocalizationParams: [this.name],
    });
  }
}
