using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.Interfaces;
using System;
using System.Collections.Generic;

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
                if(value == true)
                {
                    _done = value;
                    DomainEventStore.Add(new TodoItemCompletedEvent(this));
                };
            }
        }
        public DateTime? Reminder { get; set; }

        public PriorityLevel Priority { get; set; }

        public List<IEvent> DomainEventStore { get; set; }

        public TodoList List { get; set; }
        public TodoItem()
        {
            DomainEventStore = new List<IEvent>();
        }
    }
}
