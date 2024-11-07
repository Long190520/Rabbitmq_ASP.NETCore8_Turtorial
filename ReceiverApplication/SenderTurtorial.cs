using CommonWork;
using MassTransit;

namespace ReceiverApplication
{
    public class SenderTurtorial : IConsumer<Product>
    {
        public async Task Consume(ConsumeContext<Product> context)
        {
            var product = context.Message;
        }
    }
}
