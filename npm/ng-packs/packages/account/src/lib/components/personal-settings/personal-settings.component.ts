import { GetProfile, Profile, ProfileState, UpdateProfile } from '@aiwins/ng.core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { take, withLatestFrom, finalize } from 'rxjs/operators';
import { ToasterService } from '@aiwins/ng.theme.shared';
import { Account } from '../../models/account';

const { maxLength, required, email } = Validators;

@Component({
  selector: 'rocket-personal-settings-form',
  templateUrl: './personal-settings.component.html',
  exportAs: 'rocketPersonalSettingsForm',
})
export class PersonalSettingsComponent
  implements
    OnInit,
    Account.PersonalSettingsComponentInputs,
    Account.PersonalSettingsComponentOutputs {
  @Select(ProfileState.getProfile)
  profile$: Observable<Profile.Response>;

  form: FormGroup;

  inProgress: boolean;

  constructor(
    private fb: FormBuilder,
    private store: Store,
    private toasterService: ToasterService,
  ) {}

  ngOnInit() {
    this.buildForm();
  }

  buildForm() {
    this.store
      .dispatch(new GetProfile())
      .pipe(withLatestFrom(this.profile$), take(1))
      .subscribe(([, profile]) => {
        this.form = this.fb.group({
          userName: [profile.userName, [required, maxLength(256)]],
          email: [profile.email, [required, email, maxLength(256)]],
          name: [profile.name || '', [maxLength(64)]],
          surname: [profile.surname || '', [maxLength(64)]],
          phoneNumber: [profile.phoneNumber || '', [maxLength(16)]],
        });
      });
  }

  submit() {
    if (this.form.invalid) return;
    this.inProgress = true;
    this.store
      .dispatch(new UpdateProfile(this.form.value))
      .pipe(finalize(() => (this.inProgress = false)))
      .subscribe(() => {
        this.toasterService.success('RocketAccount::PersonalSettingsSaved', 'Success', { life: 5000 });
      });
  }
}
