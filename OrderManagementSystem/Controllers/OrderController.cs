using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Business.Models;
using OrderManagementSystem.Business.Models.OrderStateMachine.Requests;
using OrderManagementSystem.Business.Services;
using OrderManagementSystem.Common.Loggers;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("order/")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IOrderServices orderServices;
        
        public OrderController(ILogger logger, IOrderServices orderServices)
        {
            this.orderServices = orderServices;
            this.logger = logger;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrderAsync()
        {
            try
            {
                var orderStateMachine = await orderServices.GetOrderStateMachine();
                var creationResult = orderStateMachine.CreateOrder();
                if (creationResult.Success)
                {
                    return Created("", creationResult.CreatedResourceId);
                }
               
                return BadRequest(creationResult.Message);               
            }
            catch (Exception ex)
            {
                logger.LogException(LogEvent.FailedCreateOrde, ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("{orderId}/contactdetails/create")]
        public async Task<IActionResult> UpdateContactDetails([FromRouteAttribute] string orderId, [FromBody] OrderContactDetailsRequest request)
        {
            try
            {
                var orderStateMachine = await orderServices.GetOrderStateMachine(orderId);
                var result = orderStateMachine.UpdateContactDetails(request);
                return ResponseMessage(result);
            }
            catch (Exception ex)
            {
                logger.LogException(LogEvent.FailedUpdateContactDetails, ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("{orderId}/contactdetails/error")]
        public async Task<IActionResult> UpdateContactDetailsFailed([FromRouteAttribute] string orderId)
        {
            try
            {
                var orderStateMachine = await orderServices.GetOrderStateMachine(orderId);
                var result = orderStateMachine.MarkContactDetailsFailed();
                return ResponseMessage(result);
            }
            catch (Exception ex)
            {
                logger.LogException(LogEvent.FailedUpdateContactToError, ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("{orderId}/paymentdetails/create")]
        public async Task<IActionResult> UpdatePaymentDetails([FromRouteAttribute] string orderId, [FromBody] OrderPaymentDetailsRequest request)
        {
            try
            {
                var orderStateMachine = await orderServices.GetOrderStateMachine(orderId);
                var result = orderStateMachine.UpdatePaymentDetails(request);
                return ResponseMessage(result);
            }
            catch (Exception ex)
            {
                logger.LogException(LogEvent.FailedUpdatePaymentDetails, ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("{orderId}/paymentdetails/error")]
        public async Task<IActionResult> UpdatePaymentDetailsFailed([FromRouteAttribute] string orderId)
        {
            try
            {
                var orderStateMachine = await orderServices.GetOrderStateMachine(orderId);
                var result = orderStateMachine.MarkPaymentDetailsFailed();
                return ResponseMessage(result);
            }
            catch (Exception ex)
            {
                logger.LogException(LogEvent.FailedUpdatePaymentDetailsError, ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("{orderId}/deliverydetails/create")]
        public async Task<IActionResult> UpdateDeliveryDetails([FromRouteAttribute] string orderId, [FromBody] OrderDeliveryDetailsRequest request)
        {
            try
            {
                var orderStateMachine = await orderServices.GetOrderStateMachine(orderId);
                var result = orderStateMachine.UpdateDeliveryDetails(request);
                return ResponseMessage(result);
            }
            catch (Exception ex)
            {
                logger.LogException(LogEvent.FailedUUpdateDeliveryDetails, ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("{orderId}/deliverydetails/error")]
        public async Task<IActionResult> UpdateDeliveryDetailsFailed([FromRouteAttribute] string orderId)
        {
            try
            {
                var orderStateMachine = await orderServices.GetOrderStateMachine(orderId);
                var result = orderStateMachine.MarkDeliveryDetailsFailed();
                return ResponseMessage(result);
            }
            catch (Exception ex)
            {
                logger.LogException(LogEvent.FailedUpdateDeliveryDetailsError, ex);
                return new StatusCodeResult(500);
            }
        }

        private IActionResult ResponseMessage(StateResponse result)
        {
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPost]
        [Route("{orderid}/retrieve")]
        public async Task<IActionResult> Retrieve([FromRouteAttribute] string orderId)
        {
            //It should call orderServices that will retrieve order domain model, map it DTO and return it here.
            throw new NotImplementedException();
        }

    }
}
