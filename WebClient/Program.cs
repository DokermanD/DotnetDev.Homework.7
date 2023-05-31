using RestSharp;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebClient
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(
                    "\n Для получения данных покупателя введите команду - Get\n" +
                    " Для записи данных покупателя введите команду - Post\n" +
                    " Для удаления данных покупателя введите команду - Del\n" +
                    " Для вывода всех данных по покупателям введите команду - GetAll\n");

            while (true)
            {
                var comand = Console.ReadLine().ToLower();
                
                switch (comand)
                {
                    case "get":
                        Console.Write("Введите id покупателя: ");
                        var rezultGet =  int.TryParse(Console.ReadLine(), out int idGet);
                        if (rezultGet) await GetCustomerId(idGet);
                        else Console.WriteLine("Не коректный ввод id!\nПопробуйте ещё раз.\n");
                        break;

                    case "post":
                        Console.WriteLine("Генерация нового покупателя и добавление в базу.\n");
                        await PostCustomerGenerateRondom();
                        break;

                    case "del":
                        Console.Write("Введите id покупателя: ");
                        var rezultDel = int.TryParse(Console.ReadLine(), out int idDel);
                        if (rezultDel) DeleteCustomerId(idDel);
                        else Console.WriteLine("Не коректный ввод id!\nПопробуйте ещё раз.\n");
                        break;
                        
                    case "getall":
                        Console.WriteLine("\nВсе покупатели в базе: ");
                        await GetCustomerAll();
                        break;

                    default:
                        Console.WriteLine(
                            "**********************************\n"+
                            "*Введённой команды не существует!*\n"+
                            "**********************************\n\n");
                        break;
                }
            }

        }
        // Рандомная генерация покупателя
        private static CustomerCreateRequest RandomCustomer()
        {
            var (id, firstName, lastName) = RandomCuatomerGenerate.Generate();
            CustomerCreateRequest customerCreate = new CustomerCreateRequest(id,firstName, lastName);

            return customerCreate;
        }

        // GET запрос на получение всех данных по покупателям из базы данных
        private static async Task GetCustomerAll()
        {
            var client = new RestClient("https://localhost:5001");
            var request = new RestRequest($"customers/getall", Method.Get);

            var response = await client.ExecuteGetAsync(request);

            var obj = JArray.Parse(response.Content);
            
            foreach (var item in obj)
            {
                Customer showCustomer = JsonConvert.DeserializeObject<Customer>(item.ToString());

                Console.WriteLine($"{showCustomer.Id} - {showCustomer.Firstname} - {showCustomer.Lastname}");
            }


        }

        // GET запрос на получение данных по id
        private static async Task GetCustomerId(int id)
        {
            var client = new RestClient("https://localhost:5001");
            var request = new RestRequest($"customers/get/{id}", Method.Get);

            var response = await client.ExecuteGetAsync(request);

            Customer showCustomer = JsonConvert.DeserializeObject<Customer>(response.Content);

            Console.WriteLine($"{showCustomer.Id} - {showCustomer.Firstname} - {showCustomer.Lastname}");
            
        }

        // POS запрос на рондомное создание покупалеля и запись в базу
        private static async Task PostCustomerGenerateRondom()
        {            
            //Генерим запрос
            var client = new RestClient("https://localhost:5001");
            var request = new RestRequest($"customers/set", Method.Post);
            //Получаем нового покупателя и добавляем в тело запроса
            var data = JsonConvert.SerializeObject(RandomCustomer());
            request.AddBody(data);
            //Отправка запроса 
            var response = await client.ExecutePostAsync(request);
            //Проверка что ответил сервер
            var obj = JObject.Parse(response.Content);

            var idServer = obj["id"];
            var messServer = obj["message"];
            Console.WriteLine(messServer);

            await GetCustomerId(Convert.ToInt32(idServer));
        }

        // DELETE запрос на удаление покупателя по id
        private static void DeleteCustomerId(int id)
        {
            var client = new RestClient("https://localhost:5001");
            var request = new RestRequest($"customers/delete/{id}", Method.Delete);

            var response = client.Delete(request);

            var showMessage = JObject.Parse(response.Content);
            var mess = showMessage["message"];
            Console.WriteLine($"{mess}");

        }
    }
}