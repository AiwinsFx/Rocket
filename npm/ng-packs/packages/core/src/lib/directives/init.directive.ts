import { Directive, Output, EventEmitter, ElementRef, AfterViewInit } from '@angular/core';

@Directive({ selector: '[rocketInit]' })
export class InitDirective implements AfterViewInit {
  @Output('rocketInit') readonly init = new EventEmitter<ElementRef<any>>();

  constructor(private elRef: ElementRef) {}

  ngAfterViewInit() {
    this.init.emit(this.elRef);
  }
}
