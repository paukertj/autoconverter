// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paukertj.Autoconverter.Demo;
using Paukertj.Autoconverter.Demo.Services.Demo;

Console.WriteLine("Hello, World!");

using IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((_, services) =>
		services
			.AddSingleton<IDemoService, DemoService>()
			.AddAutomapping())
	.Build();

host.Services
	.GetRequiredService<IDemoService>()
	.SomeMethod();
