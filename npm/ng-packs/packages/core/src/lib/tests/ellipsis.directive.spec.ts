import { createDirectiveFactory, SpectatorDirective } from '@ngneat/spectator/jest';
import { EllipsisDirective } from '../directives/ellipsis.directive';

describe('EllipsisDirective', () => {
  let spectator: SpectatorDirective<EllipsisDirective>;
  let directive: EllipsisDirective;
  let el: HTMLDivElement;
  const createDirective = createDirectiveFactory({
    directive: EllipsisDirective,
  });

  beforeEach(() => {
    spectator = createDirective(
      '<div [rocketEllipsis]="width" [rocketEllipsisEnabled]="true" [title]="title">test content</div>',
      {
        hostProps: {
          title: 'test title',
          width: '100px',
        },
      },
    );
    directive = spectator.directive;
    el = spectator.query('div');
  });

  test('should be created', () => {
    expect(directive).toBeTruthy();
  });

  test('should have 100px ellipsis width', () => {
    expect(directive.width).toBe('100px');
  });

  test('should be enabled if rocketEllipsisEnabled input is true', () => {
    expect(directive.enabled).toBe(true);
  });

  test('should have given title', () => {
    expect(directive.title).toBe('test title');
  });

  test('should have element innerText as title if not specified', () => {
    spectator.setHostInput({ title: undefined });
    expect(directive.title).toBe(el.innerText);
  });

  test('should add rocket-ellipsis-inline class to element if width is given', () => {
    expect(el).toHaveClass('rocket-ellipsis-inline');
  });

  test('should add rocket-ellipsis class to element if width is not given', () => {
    spectator.setHostInput({ width: undefined });
    expect(el).toHaveClass('rocket-ellipsis');
  });
});
