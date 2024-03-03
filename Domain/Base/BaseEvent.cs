using MediatR;

namespace Domain.Base
{
    public abstract record BaseEvent : INotification
    {
    }
}
