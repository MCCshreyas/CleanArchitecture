using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;
using System;
using System.Collections.Generic;
using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Domain.Entities
{
    public class TodoItem : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        private bool _done;

        public bool Done
        {
            get => _done;
            set
            {
                if (value == true && _done == false)
                {
                    DomainEvents.Add(new TodoItemCompletedEvent(this));
                }
                _done = value;
            }
        }

        public DateTime? Reminder { get; set; }

        public PriorityLevel Priority { get; set; }

        public TodoList List { get; set; }

        public List<Event> DomainEvents { get; set; }

        public TodoItem()
        {
            DomainEvents = new List<Event>();
        }
    }
}