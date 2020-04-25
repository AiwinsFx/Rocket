import { ROCKET } from '../models/common';

export function organizeRoutes(
  routes: ROCKET.FullRoute[],
  wrappers: ROCKET.FullRoute[] = [],
  parentNameArr = [] as ROCKET.FullRoute[],
  parentName: string = null,
): ROCKET.FullRoute[] {
  const filter = route => {
    if (route.children && route.children.length) {
      route.children = organizeRoutes(route.children, wrappers, parentNameArr, route.name);
    }

    if (route.parentName && route.parentName !== parentName) {
      parentNameArr.push(route);
      return false;
    }

    return true;
  };

  if (parentName) {
    // recursive block
    return routes.filter(filter);
  }

  const filteredRoutes = routes.filter(filter);

  if (parentNameArr.length) {
    return sortRoutes(setChildRoute([...filteredRoutes, ...wrappers], parentNameArr));
  }

  return filteredRoutes;
}

export function setChildRoute(
  routes: ROCKET.FullRoute[],
  parentNameArr: ROCKET.FullRoute[],
): ROCKET.FullRoute[] {
  return routes.map(route => {
    if (route.children && route.children.length) {
      route.children = setChildRoute(route.children, parentNameArr);
    }

    const foundedChildren = parentNameArr.filter(parent => parent.parentName === route.name);
    if (foundedChildren && foundedChildren.length) {
      route.children = [...(route.children || []), ...foundedChildren];
    }

    return route;
  });
}

export function sortRoutes(routes: ROCKET.FullRoute[] = []): ROCKET.FullRoute[] {
  if (!routes.length) return [];
  return routes
    .map((route, index) => {
      return {
        ...route,
        order: typeof route.order === 'undefined' ? index + 1 : route.order,
      };
    })
    .sort((a, b) => a.order - b.order)
    .map(route => {
      if (route.children && route.children.length) {
        route.children = sortRoutes(route.children);
      }

      return route;
    });
}

const ROCKET_ROUTES = [] as ROCKET.FullRoute[];

export function addRocketRoutes(routes: ROCKET.FullRoute | ROCKET.FullRoute[]): void {
  if (!Array.isArray(routes)) {
    routes = [routes];
  }

  ROCKET_ROUTES.push(...routes);
}

export function getRocketRoutes(): ROCKET.FullRoute[] {
  return ROCKET_ROUTES;
}
