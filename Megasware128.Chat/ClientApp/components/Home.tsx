import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Subject, Observable } from 'rxjs';
import { HubConnection } from '@aspnet/signalr-client';

export class Home extends React.Component<RouteComponentProps<{}>, { messages: string[] }> {
  private subject = new Subject<string>();
  private hubConnection: HubConnection;

  constructor() {
    super();

    this.state = { messages: [] };

    this.hubConnection = new HubConnection('http://localhost:49980/chat');
    this.hubConnection.start().then(() => {
      const messages = new Subject<string>();

      this.hubConnection.stream<string>('messages').subscribe(messages);

      this.subject.merge(messages).subscribe(m => this.setState({ messages: this.state.messages.concat(m) }));
    });
  }

  public render() {
    return <div>
      <ul>{this.state.messages.map(m => <li>{m}</li>)}</ul>
      <input type="text" onKeyDown={this.sendMessage.bind(this)} />
    </div>;
  }

  private async sendMessage(event: React.KeyboardEvent<HTMLInputElement>) {
    if (event.which === 13 && event.target instanceof HTMLInputElement) {
      var message = event.target.value;
      await this.hubConnection.invoke('sendMessage', message);
      this.subject.next(message);
    }
  }
}
