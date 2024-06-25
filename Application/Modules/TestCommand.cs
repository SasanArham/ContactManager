using Application.Base;
using MassTransit;
using MediatR;

namespace Application.Modules
{
    public class TestCommand : IRequest<Unit>
    {
    }

    public class TestCommandHandler : IRequestHandler<TestCommand, Unit>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnitOfWork _unitOfWork;

        public TestCommandHandler(IPublishEndpoint publishEndpoint
            , IUnitOfWork unitOfWork)
        {
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            //Console.WriteLine("******** Publishing the messge");
            //await _publishEndpoint.Publish(new TestMessage { Message = "Hello sasan" });
            //await _unitOfWork.SaveChangesAsync();
            //Console.WriteLine("******** Published the messge");
            //return Unit.Value;
            throw new NotImplementedException();
        }
    }
}
