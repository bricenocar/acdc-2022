import * as React from 'react';
import styles from './TeamsTab.module.scss';
import { ITeamsTabProps } from './ITeamsTabProps';
import { escape } from '@microsoft/sp-lodash-subset';
import Content from './subcomponents/Content';

export default class TeamsTab extends React.Component<ITeamsTabProps, {}> {
  public render(): React.ReactElement<ITeamsTabProps> {
    return (
      <div className="ms-Grid" dir="ltr">
        <div className="ms-Grid-row">
          <div className="main-element ms-Grid-col ms-sm12 ms-xl12">           
            <div className="ms-Grid-row">
              <div className="ms-Grid-col ms-sm12 ms-xl12">
                <Content />
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}
