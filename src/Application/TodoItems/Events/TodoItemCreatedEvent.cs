using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Application.TodoItems.Events
{
    /// <summary>
    /// Domain event gets fired when Todo item is created
    /// </summary>
    public class TodoItemCreatedEvent : IEvent
    {
        private readonly TodoItem _itemAdded;

        public TodoItemCreatedEvent(TodoItem itemAdded)
        {
            _itemAdded = itemAdded;
        }

        public Task HandleAsync()
        {
            Console.WriteLine($"Todo item {_itemAdded.Title} created");
            return Task.CompletedTask;
        }
    }
}
