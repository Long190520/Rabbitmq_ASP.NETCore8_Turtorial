
using CommonWork;
using MassTransit;

namespace ReceiverApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddMassTransit(x =>
            {

                x.AddConsumer<SenderTurtorial>();
                x.AddConsumer<PublisherTurtorial>();
                x.AddConsumer<RequestResponseTurtorial>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost", h =>
                    {
                        h.Username("admin"); // Use RabbitMQ username
                        h.Password("admin123"); // Use RabbitMQ password
                    });

                    cfg.ReceiveEndpoint("send-turtorial", e =>
                    {
                        e.Consumer<SenderTurtorial>(context);
                    });
                    cfg.ReceiveEndpoint("publish-turtorial", e =>
                    {
                        e.Consumer<PublisherTurtorial>(context);
                    });
                    cfg.ReceiveEndpoint("reqres-turtorial", e =>
                    {
                        e.Consumer<RequestResponseTurtorial>(context);
                    });
                });

            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
