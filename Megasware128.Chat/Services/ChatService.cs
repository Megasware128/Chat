using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Megasware128.Chat.Models;

namespace Megasware128.Chat.Services
{
  public class ChatService : IChatService
  {
    private readonly ISubject<ChatMessage> messages = new ReplaySubject<ChatMessage>();

    public IObservable<string> ObserveMessages(string connectionId)
    {
      return messages.Where(m => m.ConnectionId != connectionId).Select(m => m.Message);
    }

    public void SendMessage(string message, string connectionId)
    {
      messages.OnNext(new ChatMessage { Message = message, ConnectionId = connectionId });
    }
  }
}
