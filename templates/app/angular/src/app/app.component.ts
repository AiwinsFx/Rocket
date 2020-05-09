import { LazyLoadService, LOADING_STRATEGY } from '@rocket/ng.core';
import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-root',
  template: `
    <rocket-loader-bar></rocket-loader-bar>
    <router-outlet></router-outlet>
  `,
})
export class AppComponent implements OnInit {
  constructor(private lazyLoadService: LazyLoadService) {}

  ngOnInit() {
    forkJoin(
      this.lazyLoadService.load(
        LOADING_STRATEGY.PrependAnonymousStyleToHead('fontawesome-v4-shims.min.css')
      ),
      this.lazyLoadService.load(
        LOADING_STRATEGY.PrependAnonymousStyleToHead('fontawesome-all.min.css')
      )
    ).subscribe();
  }
}
