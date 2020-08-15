using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Domain.Events
{
    public class TodoItemCompletedEvent : Event
    {
        private readonly TodoItem _item;

        public TodoItemCompletedEvent(TodoItem item)
        {
            _item = item;
        }
    }
}