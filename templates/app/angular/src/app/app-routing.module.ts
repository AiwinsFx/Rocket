import { ROCKET } from '@aiwins/ng.core';
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
    loadChildren: () => import('@aiwins/ng.account').then(m => m.AccountModule)
  },
  {
    path: 'identity',
    loadChildren: () => import('@aiwins/ng.identity').then(m => m.IdentityModule)
  },
  {
    path: 'tenant-management',
    loadChildren: () => import('@aiwins/ng.tenant-management').then(m => m.TenantManagementModule)
  },
  {
    path: 'setting-management',
    loadChildren: () => import('@aiwins/ng.setting-management').then(m => m.SettingManagementModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
