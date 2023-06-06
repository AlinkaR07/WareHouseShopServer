using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreApp.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DAL.Repository;
using System.Configuration;
using AspNetCoreApp.BLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreApp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IDbCrud dbo;
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор контроллера OrdersController
        /// </summary>
        /// <param name="newDbRepos">Переменная репозитория</param>
        /// <param name="logger">Перемнная журнала логирования</param>
        public OrdersController(IDbCrud dbCrud, ILogger<OrdersController> logger)
        {
            _logger = logger;
            dbo = dbCrud;
        }

        /// <summary>
        /// GET запрос получения списка заказов(Order)
        /// </summary>
        /// <returns>_orderRepository.OrderRepository.Get().ToList() - список заказов</returns>

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            // Информационное сообщение журнала
            _logger.LogInformation("OrderController is Invoked with the function GetOrder()");
            try
            {
                _logger.LogInformation("Request GET completed successfully");
                return dbo.AllGetOrders().ToList();
            }
            catch(Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }            
        }

        /// <summary>
        /// GET запрос получения списка заказа(Order) по номеру
        /// </summary>
        /// <param name="number">Номер заказа</param>
        /// <returns>order - найденный по номеру заказа</returns>

        // GET: 
        [HttpGet("{number}")]
        public async Task<ActionResult<Order>> GetOrder(int number)
        {
            _logger.LogInformation("OrderController is Invoked with the function GetOrder(int number)");
            try
            {
                var order = dbo.GetOrder(number);

                if (order == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Request GET completed successfully");
                return order;
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// PUT запрос изменения заказа(Order) по номеру
        /// </summary>
        /// <param name="number">номер заказа</param>
        /// <param name="order">объект Заказ, который будет изменяться</param>
        /// <returns>Измененный заказ</returns>

        // PUT: 
        [HttpPut("{number}")]
        public async Task<ActionResult<IEnumerable<Order>>> PutOrder(int number, Order order)
        {
            _logger.LogInformation("OrderController is Invoked with the function PutOrder(int number, Order order)");

            try
            {
                if (number != order.Number)
                {
                    return BadRequest();
                }
                dbo.UpdateOrder(order);

                try
                {
                    dbo.Save();
                    _logger.LogInformation("Request PUT completed successfully");
                    return dbo.AllGetOrders().ToList();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(number))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// POST запрос добавления нового заказа(Order) в БД
        /// </summary>
        /// <param name="order">Объект Заказ, который будет добавлен</param>
        /// <returns>"GetOrder", new { number = order.Number }, order - новый заказ</returns>

        // POST: 
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _logger.LogInformation("OrderController is Invoked with the function PostOrder(Order order)");

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                    dbo.CreateOrder(order);
                    dbo.Save();
                
                _logger.LogInformation("Request POST completed successfully");

                return CreatedAtAction("GetOrder", new { number = order.Number }, order);
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// DELETE запрос удаления заказа(Order) из БД по номеру заказа
        /// </summary>
        /// <param name="number">номер заказа</param>
        /// <returns></returns>

        // DELETE: 
        [HttpDelete("{number}")]
        public async Task<IActionResult> DeleteOrder(int number)
        {
            _logger.LogInformation("OrderController is Invoked with the function DeleteOrder(int number)");

            try
            {
                var order = dbo.GetOrder(number);

                if (order == null)
                {
                    return NotFound();
                }
                dbo.DeleteOrder(number); 
                dbo.Save();

                _logger.LogInformation("Request DELETE completed successfully");

                return NoContent();
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// Функция проверки существования заказа(Order) по номеру
        /// </summary>
        /// <param name="number"> номер заказа</param>
        /// <returns>Order - заказ, если он существует</returns>

        private bool OrderExists(int number)
        {
            _logger.LogInformation("OrderController is Invoked");
            try
            {
                _logger.LogInformation("Exists completed successfully");
                return dbo.GetOrder(number) != null;
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }
    }
}
