import { createHttpFactory, HttpMethod, SpectatorHttp } from '@ngneat/spectator/jest';
import { AccountService } from '../services/account.service';
import { Store } from '@ngxs/store';
import { RestService } from '@rocket/ng.core';
import { RegisterRequest } from '../models/user';

describe('AccountService', () => {
  let spectator: SpectatorHttp<AccountService>;
  const createHttp = createHttpFactory({
    dataService: AccountService,
    providers: [RestService],
    mocks: [Store],
  });

  beforeEach(() => (spectator = createHttp()));

  it('should send a GET to find tenant', () => {
    spectator.get(Store).selectSnapshot.andReturn('https://rocket.cn');
    spectator.service.findTenant('test').subscribe();
    spectator.expectOne('https://rocket.cn/api/rocket/multi-tenancy/tenants/by-name/test', HttpMethod.GET);
  });

  it('should send a POST to register API', () => {
    const mock = {
      userName: 'test',
      emailAddress: 'test@test.com',
      password: 'test1234',
      appName: 'Angular',
    } as RegisterRequest;
    spectator.get(Store).selectSnapshot.andReturn('https://rocket.cn');
    spectator.service.register(mock).subscribe();
    const req = spectator.expectOne('https://rocket.cn/api/account/register', HttpMethod.POST);
    expect(req.request.body).toEqual(mock);
  });
});
