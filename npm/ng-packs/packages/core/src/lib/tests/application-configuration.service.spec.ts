import { createHttpFactory, HttpMethod, SpectatorHttp } from '@ngneat/spectator/jest';
import { ApplicationConfigurationService, RestService } from '../services';
import { Store } from '@ngxs/store';

describe('ApplicationConfigurationService', () => {
  let spectator: SpectatorHttp<ApplicationConfigurationService>;
  const createHttp = createHttpFactory({
    dataService: ApplicationConfigurationService,
    providers: [RestService],
    mocks: [Store],
  });

  beforeEach(() => (spectator = createHttp()));

  it('should send a GET to application-configuration API', () => {
    spectator.get(Store).selectSnapshot.andReturn('https://rocket.io');
    spectator.service.getConfiguration().subscribe();
    spectator.expectOne('https://rocket.io/api/rocket/application-configuration', HttpMethod.GET);
  });
});
