import { Inject, Injectable, InjectionToken } from '@angular/core';
import { Router, Routes } from '@angular/router';
import {
  actionMatcher,
  InitState,
  NgxsNextPluginFn,
  NgxsPlugin,
  setValue,
  UpdateState,
} from '@ngxs/store';
import clone from 'just-clone';
import snq from 'snq';
import { ROCKET } from '../models';
import { getRocketRoutes, organizeRoutes } from '../utils/route-utils';

export const NGXS_CONFIG_PLUGIN_OPTIONS = new InjectionToken('NGXS_CONFIG_PLUGIN_OPTIONS');

@Injectable()
export class ConfigPlugin implements NgxsPlugin {
  private initialized = false;

  constructor(
    @Inject(NGXS_CONFIG_PLUGIN_OPTIONS) private options: ROCKET.Root,
    private router: Router,
  ) {}

  handle(state: any, event: any, next: NgxsNextPluginFn) {
    const matches = actionMatcher(event);
    const isInitAction = matches(InitState) || matches(UpdateState);

    if (isInitAction && !this.initialized) {
      const transformedRoutes = transformRoutes(this.router.config);
      let { routes } = transformedRoutes;
      const { wrappers } = transformedRoutes;

      routes = organizeRoutes(routes, wrappers);
      const flattedRoutes = flatRoutes(clone(routes));
      state = setValue(state, 'ConfigState', {
        ...(state.ConfigState && { ...state.ConfigState }),
        ...this.options,
        routes,
        flattedRoutes,
      });

      this.initialized = true;
    }

    return next(state, event);
  }
}

function transformRoutes(routes: Routes = [], wrappers: ROCKET.FullRoute[] = []): any {
  const rocketRoutes = [...getRocketRoutes()];

  wrappers = rocketRoutes.filter(ar => ar.wrapper);
  const transformed = [] as ROCKET.FullRoute[];
  routes
    .filter(route => route.component || route.loadChildren)
    .forEach(route => {
      const rocketPackage = rocketRoutes.find(
        rocket => rocket.path.toLowerCase() === route.path.toLowerCase() && !rocket.wrapper,
      );

      const { length } = transformed;

      if (rocketPackage) {
        transformed.push(rocketPackage);
      }

      if (transformed.length === length && (route.data || {}).routes) {
        transformed.push({
          ...route.data.routes,
          path: route.path,
          name: snq(() => route.data.routes.name, route.path),
          children: route.data.routes.children || [],
        } as ROCKET.FullRoute);
      }
    });

  return { routes: setUrls(transformed), wrappers };
}

function setUrls(routes: ROCKET.FullRoute[], parentUrl?: string): ROCKET.FullRoute[] {
  if (parentUrl) {
    // recursive block
    return routes.map(route => ({
      ...route,
      url: `${parentUrl}/${route.path}`,
      ...(route.children &&
        route.children.length && {
          children: setUrls(route.children, `${parentUrl}/${route.path}`),
        }),
    }));
  }

  return routes.map(route => ({
    ...route,
    url: `/${route.path}`,
    ...(route.children &&
      route.children.length && {
        children: setUrls(route.children, `/${route.path}`),
      }),
  }));
}

function flatRoutes(routes: ROCKET.FullRoute[]): ROCKET.FullRoute[] {
  const flat = (r: ROCKET.FullRoute[]) => {
    return r.reduce((acc, val) => {
      let value: ROCKET.FullRoute[] = [val];
      if (val.children) {
        val.children = val.children.map(child => ({ ...child, parentName: val.name }));
        value = [val, ...flat(val.children)];
      }

      return [...acc, ...value];
    }, []);
  };

  return flat(routes);
}
