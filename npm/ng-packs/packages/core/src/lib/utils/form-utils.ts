import { ROCKET } from '../models/common';
import { isNumber } from './number-utils';

export function mapEnumToOptions<T>(_enum: T): ROCKET.Option<T>[] {
  const options: ROCKET.Option<T>[] = [];

  for (const member in _enum)
    if (!isNumber(member))
      options.push({
        key: member,
        value: _enum[member],
      });

  return options;
}
