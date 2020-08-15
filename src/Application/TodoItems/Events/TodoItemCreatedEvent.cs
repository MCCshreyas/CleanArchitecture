using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Application.TodoItems.Events
{
    public class TodoItemCreatedEvent : Event
    {
        private readonly TodoItem _item;

        public TodoItemCreatedEvent(TodoItem item)
        {
            _item = item;
        }
    }
}