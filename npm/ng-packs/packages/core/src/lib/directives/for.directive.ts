import {
  Directive,
  EmbeddedViewRef,
  Input,
  IterableChangeRecord,
  IterableChanges,
  IterableDiffer,
  IterableDiffers,
  OnChanges,
  TemplateRef,
  TrackByFunction,
  ViewContainerRef,
} from '@angular/core';
import compare from 'just-compare';
import clone from 'just-clone';

export type CompareFn<T = any> = (value: T, comparison: T) => boolean;

class RocketForContext {
  constructor(public $implicit: any, public index: number, public count: number, public list: any[]) {}
}

class RecordView {
  constructor(public record: IterableChangeRecord<any>, public view: EmbeddedViewRef<RocketForContext>) {}
}

@Directive({
  selector: '[rocketFor]',
})
export class ForDirective implements OnChanges {
  @Input('rocketForOf')
  items: any[];

  @Input('rocketForOrderBy')
  orderBy: string;

  @Input('rocketForOrderDir')
  orderDir: 'ASC' | 'DESC';

  @Input('rocketForFilterBy')
  filterBy: string;

  @Input('rocketForFilterVal')
  filterVal: any;

  @Input('rocketForTrackBy')
  trackBy;

  @Input('rocketForCompareBy')
  compareBy: CompareFn;

  @Input('rocketForEmptyRef')
  emptyRef: TemplateRef<any>;

  private differ: IterableDiffer<any>;

  private isShowEmptyRef: boolean;

  get compareFn(): CompareFn {
    return this.compareBy || compare;
  }

  get trackByFn(): TrackByFunction<any> {
    return this.trackBy || ((index: number, item: any) => (item as any).id || index);
  }

  constructor(
    private tempRef: TemplateRef<RocketForContext>,
    private vcRef: ViewContainerRef,
    private differs: IterableDiffers,
  ) {}

  private iterateOverAppliedOperations(changes: IterableChanges<any>) {
    const rw: RecordView[] = [];

    changes.forEachOperation((record: IterableChangeRecord<any>, previousIndex: number, currentIndex: number) => {
      if (record.previousIndex == null) {
        const view = this.vcRef.createEmbeddedView(
          this.tempRef,
          new RocketForContext(null, -1, -1, this.items),
          currentIndex,
        );

        rw.push(new RecordView(record, view));
      } else if (currentIndex == null) {
        this.vcRef.remove(previousIndex);
      } else {
        const view = this.vcRef.get(previousIndex);
        this.vcRef.move(view, currentIndex);

        rw.push(new RecordView(record, view as EmbeddedViewRef<RocketForContext>));
      }
    });

    for (let i = 0, l = rw.length; i < l; i++) {
      rw[i].view.context.$implicit = rw[i].record.item;
    }
  }

  private iterateOverAttachedViews(changes: IterableChanges<any>) {
    for (let i = 0, l = this.vcRef.length; i < l; i++) {
      const viewRef = this.vcRef.get(i) as EmbeddedViewRef<RocketForContext>;
      viewRef.context.index = i;
      viewRef.context.count = l;
      viewRef.context.list = this.items;
    }

    changes.forEachIdentityChange((record: IterableChangeRecord<any>) => {
      const viewRef = this.vcRef.get(record.currentIndex) as EmbeddedViewRef<RocketForContext>;
      viewRef.context.$implicit = record.item;
    });
  }

  private projectItems(items: any[]): void {
    if (!items.length && this.emptyRef) {
      this.vcRef.clear();
      // tslint:disable-next-line: no-unused-expression
      this.vcRef.createEmbeddedView(this.emptyRef).rootNodes;
      this.isShowEmptyRef = true;
      this.differ = null;

      return;
    }

    if (this.emptyRef && this.isShowEmptyRef) {
      this.vcRef.clear();
      this.isShowEmptyRef = false;
    }

    if (!this.differ && items) {
      this.differ = this.differs.find(items).create(this.trackByFn);
    }

    if (this.differ) {
      const changes = this.differ.diff(items);

      if (changes) {
        this.iterateOverAppliedOperations(changes);
        this.iterateOverAttachedViews(changes);
      }
    }
  }

  private sortItems(items: any[]) {
    if (this.orderBy) {
      items.sort((a, b) => (a[this.orderBy] > b[this.orderBy] ? 1 : a[this.orderBy] < b[this.orderBy] ? -1 : 0));
    } else {
      items.sort();
    }
  }

  ngOnChanges() {
    let items = clone(this.items) as any[];
    if (!Array.isArray(items)) return;

    const compareFn = this.compareFn;

    if (typeof this.filterBy !== 'undefined' && typeof this.filterVal !== 'undefined' && this.filterVal !== '') {
      items = items.filter(item => compareFn(item[this.filterBy], this.filterVal));
    }

    switch (this.orderDir) {
      case 'ASC':
        this.sortItems(items);
        this.projectItems(items);
        break;

      case 'DESC':
        this.sortItems(items);
        items.reverse();
        this.projectItems(items);
        break;

      default:
        this.projectItems(items);
    }
  }
}
