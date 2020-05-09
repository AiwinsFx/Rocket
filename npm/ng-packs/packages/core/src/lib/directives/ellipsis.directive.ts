import { AfterViewInit, ChangeDetectorRef, Directive, ElementRef, HostBinding, Input } from '@angular/core';

@Directive({
  selector: '[rocketEllipsis]',
})
export class EllipsisDirective implements AfterViewInit {
  @Input('rocketEllipsis')
  width: string;

  @HostBinding('title')
  @Input()
  title: string;

  @Input('rocketEllipsisEnabled')
  enabled = true;

  @HostBinding('class.rocket-ellipsis-inline')
  get inlineClass() {
    return this.enabled && this.width;
  }

  @HostBinding('class.rocket-ellipsis')
  get class() {
    return this.enabled && !this.width;
  }

  @HostBinding('style.max-width')
  get maxWidth() {
    return this.enabled && this.width ? this.width || '170px' : undefined;
  }

  constructor(private cdRef: ChangeDetectorRef, private elRef: ElementRef) {}

  ngAfterViewInit() {
    this.title = this.title || (this.elRef.nativeElement as HTMLElement).innerText;
    this.cdRef.detectChanges();
  }
}
