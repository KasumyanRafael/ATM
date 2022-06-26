using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя");
            string name=Console.ReadLine();
            Console.WriteLine("Введите сумму своих наличных");
            int sum = Convert.ToInt32(Console.ReadLine());
            BankUser bankUser = new BankUser(name, sum);
            Atm valera = new Atm(name,sum);
            Console.WriteLine("Здравствуйте, {0}. Вас приветствует наш банкомат." +
                "Если хотите пополнить свой счёт (покупюрно), жмите 1." +
                "Для оплаты госпошлины жмите 2." +
                "Для осуществления денежного перевода жмите 3." +
                "Для обналичивания суммы жмите 4." +
                "Для выхода жмите 0.",bankUser.UserName);
            string n=Console.ReadLine();
            while(n!="0")
            {
                switch(n)
                {
                    case "1":
                        Console.WriteLine("Какую купюру желаете положить на карточку?");
                        int sm=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(valera.IncreaseAccount(bankUser,sm));
                        break;
                    case "2":
                        Console.WriteLine("Доводим до сведения, что госпошлина стоит 1000 рублей. Подтвердите свои намерения.");
                        Console.WriteLine(valera.PayStateTax(bankUser));
                        break;
                    case "3":
                        Console.WriteLine("Введите имя пользователя, кому хотите перевести деньги");
                        string getter=Console.ReadLine();
                        Console.WriteLine("Какую сумму хотите перевести");
                        int trans=Convert.ToInt32(Console.ReadLine());
                        BankUser user=new BankUser(getter,0);
                        Console.WriteLine(valera.Transmission(bankUser,user,trans));
                        break;
                    case "4":
                        Console.WriteLine("Введите допустимую для обналичивания сумму");
                        int d=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(valera.IncreaseBanknotes(bankUser,d));
                        break;
                }
                n=Console.ReadLine();
            }
        }
    }
}
