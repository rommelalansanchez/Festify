using Festify.Promotion.Messages.Sales;
using Festify.Promotion.Sales;
using FluentAssertions;
using MassTransit;
using MassTransit.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Festify.Promotion.UnitTest
{

	public class PromotionServiceTests
	{
		[Fact]
		public async Task WhenCustomerPurchasesItem_ThenPurchaseIsPublished()
		{
			InMemoryTestHarness harness = new InMemoryTestHarness();

			await harness.Start();

			//Arrange

			IPublishEndpoint publishEndpoint = harness.Bus;
			var producer = new PromotionService(publishEndpoint);

			//Act
			producer.PurchaseTicket();
			await harness.InactivityTask;

			//Assert
			harness.Published.Select<OrderPlaced>()
				.Count().Should().Be(1);


			await harness.Stop();
		}

	}
}
