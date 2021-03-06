import { Router } from '@angular/router';
import { createServiceFactory, SpectatorService, SpyObject } from '@ngneat/spectator/jest';
import { Store, Actions } from '@ngxs/store';
import { Observable, of, Subject } from 'rxjs';
import { LocalizationService } from '../services/localization.service';

describe('LocalizationService', () => {
  let spectator: SpectatorService<LocalizationService>;
  let store: SpyObject<Store>;
  let service: LocalizationService;

  const createService = createServiceFactory({
    service: LocalizationService,
    entryComponents: [],
    mocks: [Store, Router],
    providers: [{ provide: Actions, useValue: new Subject() }],
  });

  beforeEach(() => {
    spectator = createService();
    store = spectator.get(Store);
    service = spectator.service;
  });

  describe('#currentLang', () => {
    it('should be tr', () => {
      store.selectSnapshot.andCallFake((selector: (state: any, ...states: any[]) => string) => {
        return selector({ SessionState: { language: 'tr' } });
      });

      expect(service.currentLang).toBe('tr');
    });
  });

  describe('#get', () => {
    it('should be return an observable localization', async () => {
      store.select.andReturn(of('RocketTest'));

      const localization = await service.get('RocketTest').toPromise();

      expect(localization).toBe('RocketTest');
    });
  });

  describe('#instant', () => {
    it('should be return a localization', () => {
      store.selectSnapshot.andCallFake(
        (selector: (state: any, ...states: any[]) => Observable<string>) => {
          return selector({
            ConfigState: { getLocalization: (keys, ...interpolateParams) => keys },
          });
        },
      );

      expect(service.instant('RocketTest')).toBe('RocketTest');
    });
  });

  describe('#registerLocale', () => {
    it('should return registerLocale and then call setRouteReuse', () => {
      const router = spectator.get(Router);

      const shouldReuseRoute = () => true;
      router.routeReuseStrategy = { shouldReuseRoute } as any;

      router.navigateByUrl.andCallFake(url => {
        return new Promise(resolve => resolve({ catch: () => null }));
      });

      service.registerLocale('tr');

      expect(router.navigated).toBe(false);
      expect(router.routeReuseStrategy.shouldReuseRoute).not.toEqual(shouldReuseRoute);
    });

    it('should throw an error message when service have an otherInstance', async () => {
      try {
        const instance = new LocalizationService(new Subject(), null, null, null, {} as any);
      } catch (error) {
        expect((error as Error).message).toBe('LocalizationService should have only one instance.');
      }
    });
  });
});
