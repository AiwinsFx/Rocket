import { ROCKET } from '../models';

export namespace Session {
  export interface State {
    language: string;
    tenant: ROCKET.BasicItem;
    sessionDetail: SessionDetail;
  }

  export interface SessionDetail {
    openedTabCount: number;
    lastExitTime: number;
    remember: boolean;
  }
}
