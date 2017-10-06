using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Megasware128.Chat.Services
{
  public interface IChatService
  {
    IObservable<string> ObserveMessages(string connectionId);
    void SendMessage(string message, string connectionId);
  }
}
