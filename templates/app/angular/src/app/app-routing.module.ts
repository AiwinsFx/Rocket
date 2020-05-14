import { ROCKET } from '@rocket/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
    data: {
      routes: {
        name: '::Menu:Home'
      } as ROCKET.Route
    }
  },
  {
    path: 'account',
    loadChildren: () => import('@rocket/ng.account').then(m => m.AccountModule)
  },
  {
    path: 'identity',
    loadChildren: () => import('@rocket/ng.identity').then(m => m.IdentityModule)
  },
  {
    path: 'tenant-management',
    loadChildren: () => import('@rocket/ng.tenant-management').then(m => m.TenantManagementModule)
  },
  {
    path: 'setting-management',
    loadChildren: () => import('@rocket/ng.setting-management').then(m => m.SettingManagementModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
