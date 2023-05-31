using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        //Лист для хронения пользователей
        private static List<Customer> _customersList = new List<Customer>();

        //Получение по id
        [HttpGet("getall")]
        public IActionResult GetCustomer()
        {
            var castomer = _customersList;

            return StatusCode(200,castomer);
        }

        //Получение по id
        [HttpGet("get/{id}")]
        public IActionResult GetCustomer([FromRoute] int id)
        {
            var castomer = _customersList.SingleOrDefault(x => x.Id == id);

            if (castomer == null)
            {
                return NotFound(new { message = $"Customer id - {id} не найден" });
            }

            return Ok(castomer);
        }

        //Создание
        [HttpPost("set")]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Проверка id в списке
            var searchId = _customersList.SingleOrDefault(x => x.Id == customer.Id);
            if (searchId != null) 
            {
                return StatusCode(409, new { id = customer.Id, message = $"Покупатель с id = {customer.Id} уже есть в базе!\n------------------------------------------------" } ); //Если id уже есть в списке
            }

            _customersList.Add(customer);
            return StatusCode(200, new {id = customer.Id, message = $"Покупатель с id = {customer.Id} успешно добавлен" });
        }
        

        //Удаление по id
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCustomer([FromRoute] int id) 
        {
            _customersList.Remove(_customersList.SingleOrDefault(x => x.Id == id));
            return StatusCode(200,new {message = $"Удалён покупатель id - {id}" });
        }    
    }
}