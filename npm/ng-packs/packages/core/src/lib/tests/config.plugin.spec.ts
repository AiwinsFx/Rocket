import { RouterTestingModule } from '@angular/router/testing';
import { createServiceFactory, SpectatorService } from '@ngneat/spectator/jest';
import { NgxsModule, Store } from '@ngxs/store';
import { OAuthModule } from 'angular-oauth2-oidc';
import { environment } from '../../../../../apps/dev-app/src/environments/environment';
import { RouterOutletComponent } from '../components';
import { CoreModule } from '../core.module';
import { eLayoutType } from '../enums/common';
import { ABP } from '../models';
import { ConfigPlugin } from '../plugins';
import { ConfigState } from '../states';
import { addRocketRoutes } from '../utils';

addRocketRoutes([
  {
    name: 'RocketUiNavigation::Menu:Administration',
    path: '',
    order: 1,
    wrapper: true,
  },
  {
    name: 'RocketIdentity::Menu:IdentityManagement',
    path: 'identity',
    order: 1,
    parentName: 'RocketUiNavigation::Menu:Administration',
    layout: eLayoutType.application,
    iconClass: 'fa fa-id-card-o',
    children: [
      { path: 'roles', name: 'RocketIdentity::Roles', order: 2, requiredPolicy: 'RocketIdentity.Roles' },
      { path: 'users', name: 'RocketIdentity::Users', order: 1, requiredPolicy: 'RocketIdentity.Users' },
    ],
  },
  {
    name: 'RocketAccount::Menu:Account',
    path: 'account',
    invisible: true,
    layout: eLayoutType.application,
    children: [
      { path: 'login', name: 'RocketAccount::Login', order: 1 },
      { path: 'register', name: 'RocketAccount::Register', order: 2 },
    ],
  },
  {
    name: 'RocketTenantManagement::Menu:TenantManagement',
    path: 'tenant-management',
    parentName: 'RocketUiNavigation::Menu:Administration',
    layout: eLayoutType.application,
    iconClass: 'fa fa-users',
    children: [
      {
        path: 'tenants',
        name: 'RocketTenantManagement::Tenants',
        order: 1,
        requiredPolicy: 'RocketTenantManagement.Tenants',
      },
    ],
  },
]);

const expectedState = {
  environment,
  routes: [
    {
      name: '::Menu:Home',
      path: '',
      children: [],
      url: '/',
      order: 1,
    },
    {
      name: 'RocketUiNavigation::Menu:Administration',
      path: '',
      order: 1,
      wrapper: true,
      children: [
        {
          name: 'RocketIdentity::Menu:IdentityManagement',
          path: 'identity',
          order: 1,
          parentName: 'RocketUiNavigation::Menu:Administration',
          layout: 'application',
          iconClass: 'fa fa-id-card-o',
          children: [
            {
              path: 'users',
              name: 'RocketIdentity::Users',
              order: 1,
              requiredPolicy: 'RocketIdentity.Users',
              url: '/identity/users',
            },
            {
              path: 'roles',
              name: 'RocketIdentity::Roles',
              order: 2,
              requiredPolicy: 'RocketIdentity.Roles',
              url: '/identity/roles',
            },
          ],
          url: '/identity',
        },
        {
          name: 'RocketTenantManagement::Menu:TenantManagement',
          path: 'tenant-management',
          parentName: 'RocketUiNavigation::Menu:Administration',
          layout: 'application',
          iconClass: 'fa fa-users',
          children: [
            {
              path: 'tenants',
              name: 'RocketTenantManagement::Tenants',
              order: 1,
              requiredPolicy: 'RocketTenantManagement.Tenants',
              url: '/tenant-management/tenants',
            },
          ],
          url: '/tenant-management',
          order: 2,
        },
      ],
    },
    {
      name: 'RocketAccount::Menu:Account',
      path: 'account',
      invisible: true,
      layout: 'application',
      children: [
        {
          path: 'login',
          name: 'RocketAccount::Login',
          order: 1,
          url: '/account/login',
        },
        {
          path: 'register',
          name: 'RocketAccount::Register',
          order: 2,
          url: '/account/register',
        },
      ],
      url: '/account',
      order: 2,
    },
  ],
  flattedRoutes: [
    {
      name: '::Menu:Home',
      path: '',
      children: [],
      url: '/',
      order: 1,
    },
    {
      name: 'RocketUiNavigation::Menu:Administration',
      path: '',
      order: 1,
      wrapper: true,
      children: [
        {
          name: 'RocketIdentity::Menu:IdentityManagement',
          path: 'identity',
          order: 1,
          parentName: 'RocketUiNavigation::Menu:Administration',
          layout: 'application',
          iconClass: 'fa fa-id-card-o',
          children: [
            {
              path: 'users',
              name: 'RocketIdentity::Users',
              order: 1,
              parentName: 'RocketIdentity::Menu:IdentityManagement',
              requiredPolicy: 'RocketIdentity.Users',
              url: '/identity/users',
            },
            {
              path: 'roles',
              name: 'RocketIdentity::Roles',
              order: 2,
              parentName: 'RocketIdentity::Menu:IdentityManagement',
              requiredPolicy: 'RocketIdentity.Roles',
              url: '/identity/roles',
            },
          ],
          url: '/identity',
        },
        {
          name: 'RocketTenantManagement::Menu:TenantManagement',
          path: 'tenant-management',
          parentName: 'RocketUiNavigation::Menu:Administration',
          layout: 'application',
          iconClass: 'fa fa-users',
          children: [
            {
              path: 'tenants',
              name: 'RocketTenantManagement::Tenants',
              order: 1,
              parentName: 'RocketTenantManagement::Menu:TenantManagement',
              requiredPolicy: 'RocketTenantManagement.Tenants',
              url: '/tenant-management/tenants',
            },
          ],
          url: '/tenant-management',
          order: 2,
        },
      ],
    },
    {
      name: 'RocketIdentity::Menu:IdentityManagement',
      path: 'identity',
      order: 1,
      parentName: 'RocketUiNavigation::Menu:Administration',
      layout: 'application',
      iconClass: 'fa fa-id-card-o',
      children: [
        {
          path: 'users',
          name: 'RocketIdentity::Users',
          order: 1,
          parentName: 'RocketIdentity::Menu:IdentityManagement',
          requiredPolicy: 'RocketIdentity.Users',
          url: '/identity/users',
        },
        {
          path: 'roles',
          name: 'RocketIdentity::Roles',
          order: 2,
          parentName: 'RocketIdentity::Menu:IdentityManagement',
          requiredPolicy: 'RocketIdentity.Roles',
          url: '/identity/roles',
        },
      ],
      url: '/identity',
    },
    {
      path: 'users',
      name: 'RocketIdentity::Users',
      order: 1,
      parentName: 'RocketIdentity::Menu:IdentityManagement',
      requiredPolicy: 'RocketIdentity.Users',
      url: '/identity/users',
    },
    {
      path: 'roles',
      name: 'RocketIdentity::Roles',
      order: 2,
      parentName: 'RocketIdentity::Menu:IdentityManagement',
      requiredPolicy: 'RocketIdentity.Roles',
      url: '/identity/roles',
    },
    {
      name: 'RocketTenantManagement::Menu:TenantManagement',
      path: 'tenant-management',
      parentName: 'RocketUiNavigation::Menu:Administration',
      layout: 'application',
      iconClass: 'fa fa-users',
      children: [
        {
          path: 'tenants',
          name: 'RocketTenantManagement::Tenants',
          order: 1,
          parentName: 'RocketTenantManagement::Menu:TenantManagement',
          requiredPolicy: 'RocketTenantManagement.Tenants',
          url: '/tenant-management/tenants',
        },
      ],
      url: '/tenant-management',
      order: 2,
    },
    {
      path: 'tenants',
      name: 'RocketTenantManagement::Tenants',
      order: 1,
      parentName: 'RocketTenantManagement::Menu:TenantManagement',
      requiredPolicy: 'RocketTenantManagement.Tenants',
      url: '/tenant-management/tenants',
    },
    {
      name: 'RocketAccount::Menu:Account',
      path: 'account',
      invisible: true,
      layout: 'application',
      children: [
        {
          path: 'login',
          name: 'RocketAccount::Login',
          order: 1,
          parentName: 'RocketAccount::Menu:Account',
          url: '/account/login',
        },
        {
          path: 'register',
          name: 'RocketAccount::Register',
          order: 2,
          parentName: 'RocketAccount::Menu:Account',
          url: '/account/register',
        },
      ],
      url: '/account',
      order: 2,
    },
    {
      path: 'login',
      name: 'RocketAccount::Login',
      order: 1,
      parentName: 'RocketAccount::Menu:Account',
      url: '/account/login',
    },
    {
      path: 'register',
      name: 'RocketAccount::Register',
      order: 2,
      parentName: 'RocketAccount::Menu:Account',
      url: '/account/register',
    },
  ],
};

describe('ConfigPlugin', () => {
  let spectator: SpectatorService<ConfigPlugin>;
  const createService = createServiceFactory({
    service: ConfigPlugin,
    imports: [
      NgxsModule.forRoot([ConfigState]),
      CoreModule.forRoot({ environment }),
      OAuthModule.forRoot(),
      RouterTestingModule.withRoutes([
        {
          path: '',
          component: RouterOutletComponent,
          data: {
            routes: {
              name: '::Menu:Home',
            } as ABP.Route,
          },
        },
        { path: 'identity', component: RouterOutletComponent },
        { path: 'account', component: RouterOutletComponent },
        { path: 'tenant-management', component: RouterOutletComponent },
      ]),
    ],
  });

  beforeEach(() => {
    spectator = createService();
  });

  it('should ConfigState must be create with correct datas', () => {
    const store = spectator.get(Store);
    const state = store.selectSnapshot(ConfigState);
    expect(state).toEqual(expectedState);
  });
});
