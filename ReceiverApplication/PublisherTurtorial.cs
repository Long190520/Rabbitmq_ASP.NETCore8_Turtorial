using CommonWork;
using MassTransit;

namespace ReceiverApplication
{
    public class PublisherTurtorial : IConsumer<Person>
    {
        public async Task Consume(ConsumeContext<Person> context)
        {
            var info = context.Message;
        }
    }
}
