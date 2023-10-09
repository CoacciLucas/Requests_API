using FluentValidation.Results;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domain.Entities
{
    public class Entity
    {
        private List<INotification> _domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public int Id { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        protected Entity()
        {
            ValidationResult = new ValidationResult();
        }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public void AddDomainNotification(string property, string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(property, message));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return ((Entity)obj).Id == Id;
        }

        public void Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            validator.Validate(model).Errors.ToList().ForEach(delegate (ValidationFailure error)
            {
                ValidationResult.Errors.Add(error);
            });
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (object.Equals(left, null))
            {
                if (!object.Equals(right, null))
                {
                    return false;
                }

                return true;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
