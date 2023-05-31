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
        //���� ��� �������� �������������
        private static List<Customer> _customersList = new List<Customer>();

        //��������� �� id
        [HttpGet("getall")]
        public IActionResult GetCustomer()
        {
            var castomer = _customersList;

            return StatusCode(200,castomer);
        }

        //��������� �� id
        [HttpGet("get/{id}")]
        public IActionResult GetCustomer([FromRoute] int id)
        {
            var castomer = _customersList.SingleOrDefault(x => x.Id == id);

            if (castomer == null)
            {
                return NotFound(new { message = $"Customer id - {id} �� ������" });
            }

            return Ok(castomer);
        }

        //��������
        [HttpPost("set")]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //�������� id � ������
            var searchId = _customersList.SingleOrDefault(x => x.Id == customer.Id);
            if (searchId != null) 
            {
                return StatusCode(409, new { id = customer.Id, message = $"���������� � id = {customer.Id} ��� ���� � ����!\n------------------------------------------------" } ); //���� id ��� ���� � ������
            }

            _customersList.Add(customer);
            return StatusCode(200, new {id = customer.Id, message = $"���������� � id = {customer.Id} ������� ��������" });
        }
        

        //�������� �� id
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCustomer([FromRoute] int id) 
        {
            _customersList.Remove(_customersList.SingleOrDefault(x => x.Id == id));
            return StatusCode(200,new {message = $"����� ���������� id - {id}" });
        }    
    }
}