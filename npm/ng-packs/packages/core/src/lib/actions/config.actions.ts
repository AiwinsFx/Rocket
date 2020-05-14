import { ROCKET } from '../models/common';
import { Config } from '../models/config';

export class PatchRouteByName {
  static readonly type = '[Config] Patch Route By Name';
  constructor(public name: string, public newValue: Partial<ROCKET.Route>) {}
}

export class GetAppConfiguration {
  static readonly type = '[Config] Get App Configuration';
}

/**
 * @see usage: https://github.com/rocketframework/rocket/pull/2425#issue-355018812
 */
export class AddRoute {
  static readonly type = '[Config] Add Route';
  constructor(public payload: Omit<ROCKET.Route, 'children'>) {}
}

export class SetEnvironment {
  static readonly type = '[Config] Set Environment';
  constructor(public environment: Config.Environment) {}
}
