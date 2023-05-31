using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient
{
    public class RandomCuatomerGenerate
    {
        public static (int id, string firstName, string lastName) Generate() 
        {
            string[] firstName = { "Дмитрий","Алексей","Евгений","Эдуард","Сергей","Даниил","Георгий","Александр","Владимир","Антон" };
            string[] lastName = { "Хаткевич", "Ильин", "Иванов", "Петров", "Сидоров", "Скрипкин", "Пушкин", "Пархомов", "Удочкин", "Земский" };

            Random random = new Random();
            var fn = random.Next(0, 9);
            var ln = random.Next(0, 9);
            var id = random.Next(1, 30);

            return (id, firstName[fn], lastName[ln]);
        }
        
    }
}
