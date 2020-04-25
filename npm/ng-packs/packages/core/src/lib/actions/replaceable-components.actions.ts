import { ReplaceableComponents } from '../models/replaceable-components';

/**
 * @see usage: https://github.com/aiwinsfx/rocket/pull/2522#issue-358333183
 */
export class AddReplaceableComponent {
  static readonly type = '[ReplaceableComponents] Add';
  constructor(public payload: ReplaceableComponents.ReplaceableComponent) {}
}
