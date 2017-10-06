using Megasware128.Chat.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Megasware128.Chat.Hubs
{
  public class ChatHub : Hub
  {
    private readonly IChatService chatService;

    public ChatHub(IChatService chatService)
    {
      this.chatService = chatService;
    }

    public IObservable<string> Messages()
    {
      return chatService.ObserveMessages(Context.ConnectionId);
    }

    public void SendMessage(string message)
    {
      chatService.SendMessage(message, Context.ConnectionId);
    }
  }
}
