using Abp.Domain.Uow;
using Application.Notification;
using Domain.Entities;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public abstract class CommandHandler
    {
        protected readonly IUnitOfWork _uow;

        protected readonly IMediator _mediator;

        protected readonly INotificationHandler<NotificationDomain> _notifications;

        protected readonly ILogger<CommandHandler> _logger;

        public CommandHandler()
        {
        }

        public CommandHandler(IMediator mediator, INotificationHandler<NotificationDomain> notifications)
        {
            _mediator = mediator;
            _notifications = notifications;
        }

        public CommandHandler(IUnitOfWork uow, IMediator mediator, INotificationHandler<NotificationDomain> notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = notifications;
        }

        public CommandHandler(IUnitOfWork uow, IMediator mediator, INotificationHandler<NotificationDomain> notifications, ILogger<CommandHandler> logger = null)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = notifications;
            _logger = logger;
        }

        protected async void HandleEntity(Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            foreach (ValidationFailure error in entity.ValidationResult.Errors)
            {
                await _mediator.Publish(new NotificationDomain(error.PropertyName, error.ErrorMessage));
            }
        }

        protected async void HandleEntities(IEnumerable<Entity> entities)
        {
            if (entities == null)
            {
                return;
            }

            foreach (Entity entity in entities)
            {
                foreach (ValidationFailure error in entity.ValidationResult.Errors)
                {
                    await _mediator.Publish(new NotificationDomain(error.PropertyName, error.ErrorMessage));
                }
            }
        }

        protected async void AddNotification(string key, string message)
        {
            await _mediator.Publish(new NotificationDomain(key, message));
        }

        public async void AddNotification(NotificationDomain notification)
        {
            await _mediator.Publish(new NotificationDomain(notification.MessageId, notification.Message));
        }

        public async void AddNotifications(IReadOnlyCollection<NotificationDomain> notifications)
        {
            foreach (NotificationDomain notification in notifications)
            {
                await _mediator.Publish(new NotificationDomain(notification.MessageId, notification.Message));
            }
        }

        public async void AddNotifications(IList<NotificationDomain> notifications)
        {
            foreach (NotificationDomain notification in notifications)
            {
                await _mediator.Publish(new NotificationDomain(notification.MessageId, notification.Message));
            }
        }

        public async void AddNotifications(ICollection<NotificationDomain> notifications)
        {
            foreach (NotificationDomain notification in notifications)
            {
                await _mediator.Publish(new NotificationDomain(notification.MessageId, notification.Message));
            }
        }

        protected bool IsSuccess()
        {
            return !((NotificationDomainHandler)_notifications).HasNotifications();
        }

        protected List<NotificationDomain> DomainNotifications()
        {
            return ((NotificationDomainHandler)_notifications).GetNotifications();
        }

        protected async Task CommittAsync()
        {
            _logger.LogTrace("Is Success Domain Notificações", IsSuccess());
            if (IsSuccess())
            {
                await _uow.CompleteAsync();
                _logger.LogTrace("Is Commit DataBase", true);
            }
        }
    }
}
