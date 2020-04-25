import { SortOrder, SortPipe } from '@rocket/ng.core';
import {
  ChangeDetectorRef,
  Directive,
  Host,
  Input,
  OnChanges,
  Optional,
  Self,
  SimpleChanges,
} from '@angular/core';
import clone from 'just-clone';
import snq from 'snq';
import { TableComponent } from '../components/table/table.component';

export interface TableSortOptions {
  key: string;
  order: SortOrder;
}

@Directive({
  selector: '[rocketTableSort]',
  providers: [SortPipe],
})
export class TableSortDirective implements OnChanges {
  @Input()
  rocketTableSort: TableSortOptions;

  @Input()
  value: any[] = [];

  get table(): TableComponent | any {
    return (
      this.rocketTable || snq(() => this.cdRef['_view'].component) || snq(() => this.cdRef['context']) // 'context' for ivy
    );
  }

  constructor(
    @Host() @Optional() @Self() private rocketTable: TableComponent,
    private sortPipe: SortPipe,
    private cdRef: ChangeDetectorRef,
  ) {}

  ngOnChanges({ value, rocketTableSort }: SimpleChanges) {
    if (this.table && (value || rocketTableSort)) {
      this.rocketTableSort = this.rocketTableSort || ({} as TableSortOptions);
      this.table.value = this.sortPipe.transform(
        clone(this.value),
        this.rocketTableSort.order,
        this.rocketTableSort.key,
      );
    }
  }
}
