using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Domain.Events
{
    /// <summary>
    /// Event gets fired when todo is done or completed
    /// </summary>
    public class TodoItemCompletedEvent : IEvent
    {
        private readonly TodoItem _itemCompleted;

        public TodoItemCompletedEvent(TodoItem itemCompleted)
        {
            _itemCompleted = itemCompleted;
        }

        public Task HandleAsync()
        {
            Console.WriteLine($"{_itemCompleted.Title} completed");
            return Task.CompletedTask;
        }
    }
}
