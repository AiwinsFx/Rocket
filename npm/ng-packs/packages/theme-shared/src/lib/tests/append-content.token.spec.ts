import { Component } from '@angular/core';
import { createComponentFactory, Spectator } from '@ngneat/spectator';
import { THEME_SHARED_APPEND_CONTENT } from '../tokens/append-content.token';
import { DomInsertionService } from '@aiwins/ng.core';
import { chartJsLoaded$ } from '../utils';
import styles from '../constants/styles';

@Component({ selector: 'rocket-dummy', template: '' })
class DummyComponent {}

describe('AppendContentToken', () => {
  let spectator: Spectator<DummyComponent>;
  const createComponent = createComponentFactory(DummyComponent);

  beforeEach(() => (spectator = createComponent()));

  it('should insert a style element to the DOM', () => {
    spectator.get(THEME_SHARED_APPEND_CONTENT);
    expect(spectator.get(DomInsertionService).has(styles)).toBe(true);
  });

  it('should be loaded the chart.js', done => {
    chartJsLoaded$.subscribe(loaded => {
      expect(loaded).toBe(true);
      done();
    });

    spectator.get(THEME_SHARED_APPEND_CONTENT);
  });
});
