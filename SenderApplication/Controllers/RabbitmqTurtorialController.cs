using Microsoft.AspNetCore.Mvc;
using CommonWork;
using MassTransit;

namespace SenderApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitmqTurtorialController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IRequestClient<BalanceUpdate> _requestClient;

        public RabbitmqTurtorialController(IBus bus, IRequestClient<BalanceUpdate> requestClient) {
            _bus = bus;
            _requestClient = requestClient;
        }

        // command send part

        [HttpPost("send-turtorial")]
        public async Task<IActionResult> Test1()
        {
            var product = new Product()
            {
                Name = "computer",
                Price = 500
            };

            var url = new Uri("rabbitmq://localhost/send-turtorial");
            var endpoint = await _bus.GetSendEndpoint(url);
            await endpoint.Send(product);

            return Ok("Hello command send turtorial");
        }

        [HttpPost("publish-turtorial")]
        public async Task<IActionResult> Test2()
        {
            await _bus.Publish(new Person()
            {
                Name = "Long",
                Email = "Test@gmail.com"
            });

            return Ok("publish turtorial part done!");
        }

        [HttpPost("reqres-turtorial")]
        public async Task<IActionResult> Test3()
        {
            var requestData = new BalanceUpdate()
            {
                TypeOfInstruction = "minusAmount",
                Amount = 100,
            };

            var request = _requestClient.Create(requestData);
            var response = await request.GetResponse<NowBalance>();

            return Ok(response);
        }
    }
}
