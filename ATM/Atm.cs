using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATM
{
    public class Atm :BankUser//банкомат
    {
        Dictionary<int, int> banknotes = new Dictionary<int, int>(); // хранилище купюр в банкомате, номинал - количество (изначально 500)
        public Atm(string user, int money) : base(user, money) //Доработать. Больше 30 тысяч снимать нельзя. Только через кассу.
        {
            banknotes.Add(50,500);
            banknotes.Add(100, 500);
            banknotes.Add(200, 500);
            banknotes.Add(500, 500);
            banknotes.Add(1000, 500);
            banknotes.Add(2000, 500);
            banknotes.Add(5000, 500);
        }

        public string IncreaseAccount(BankUser user,int sum) //положить деньги на карточку
        {
            if (user.UserBanknotes >= sum && banknotes.ContainsKey(sum))
            {
                UserAccount += sum;  //
                user.UserBanknotes -= sum;
                banknotes[sum]++;
                return String.Format("Вы положили на карточку {0} рублей", sum);
            }
            if (!banknotes.ContainsKey(sum))
            {
                return ("Попробуйте снова. Такой купюры нет.");
            }
            return ("У вас недостаточно средств для данной операции. Перевод не пороизошёл");
        }
        public string PayStateTax(BankUser user) //оплата пошлины
        {
            Console.WriteLine("Щёлкните 1, если используете безналичный расчёт. Щёлкните 0, если используете бумажные деньги");
            string result=Console.ReadLine();
            int sum = 1000;
            switch(result)
            {
                case "0":
                    if (user.UserBanknotes >= sum && banknotes.ContainsKey(sum))
                    {
                        user.UserBanknotes -= sum;
                        banknotes[sum]++;
                        if (!banknotes.ContainsKey(sum))
                        {
                            return ("Попробуйте снова. Такой купюры нет.");
                        }
                        return String.Format("Вы успешно оплатили госпошлину в {0} рублей", sum);
                    }
                    return String.Format("Вы успешно оплатили госпошлину в {0} рублей", sum);
               case "1":
                    if (UserAccount >= sum)  //
                    {  
                        UserAccount -= sum;  //
                        return String.Format("Вы успешно оплатили госпошлину в {0} рублей", sum);
                    }
                    return ("У вас недостаточно средств для данной операции. Перевод не пороизошёл");
            }
            return "Пожалуйста, вводите либо 1 либо 0 в зависимости от Вашего выбора";
           
        }
        public string Transmission(BankUser transmitter, BankUser getter,int transmissionSum) //денежный перевод
        {
            Console.WriteLine("Щёлкните 1, если используете безналичный расчёт. Щёлкните 0, если используете бумажные деньги");
            string result = Console.ReadLine();
            switch (result)
            {
                case "0":
                    if(transmitter.UserBanknotes >= transmissionSum && banknotes.ContainsKey(transmissionSum))
                    {
                        transmitter.UserBanknotes -= transmissionSum;
                        banknotes[transmissionSum]++;
                        if (!banknotes.ContainsKey(transmissionSum))
                        {
                            return ("Попробуйте снова. Такой купюры нет.");
                        }
                        return String.Format("Вы успешно перевели пользователю {0}  {1} рублей",getter.UserName, transmissionSum);

                    }
                    return String.Format("У вас недостаточно средств для данной операции. Перевод не пороизошёл");
                    case "1":
                        if (UserAccount >= transmissionSum) //
                        {
                            UserAccount -= transmissionSum; //
                            GetterAccount += transmissionSum; //
                            return String.Format("Вы успешно перевели пользователю {0}  {1} рублей", getter.UserName, transmissionSum);
                        }
                        return String.Format("У вас недостаточно средств для данной операции. Перевод не пороизошёл"); 
            }
            return "Пожалуйста, вводите либо 1 либо 0 в зависимости от Вашего выбора";
        }
        public string IncreaseBanknotes(BankUser user,int sum) //снятие денег с карточки
        {
            Console.WriteLine("Какими купюрами хотите получить сумму? Вводите их номиналы через строчку. Нажмите 6, когда закончите");
            List<int> temp = new List<int>();
            FillTemp(temp, banknotes);
            int usersum = SumList(temp);
            if (UserAccount >= sum && sum<=30000 && usersum==sum)
            {
                for(int i=0;i<temp.Count;i++)
                {
                    int c = banknotes[temp[i]] - 1;
                    if (c>=0)
                    {
                        banknotes[temp[i]]--;
                    }
                    else
                    {
                        for (int j = i-1; j >=0; j--)
                        {
                            banknotes[temp[j]]++;
                        }
                        return String.Format("Увы, в банкомате недостаточно купюр в {0} рублей. Нажмите 4 и повторите попытку, уже с другим набором.", temp[i]);
                    }
                }
                UserAccount -= sum;  //
                user.UserBanknotes += sum;
                return String.Format("Вы обналичили {0} рублей", sum);
            }
            if(sum>30000)
            {
                return ("Суммы более 30000 снимаются строго на кассе");
            }
            if(sum!=usersum)
            {
                return String.Format("Выбранные вами купюры не составляют {0} рублей. Ещё раз нажмите 4 для повторения попытки",sum);
            }
            return ("У вас недостаточно средств для данной операции. Пополните свой аккаунт");
        }
        static void FillTemp(List<int>temp,Dictionary<int,int>banknotes)
        {
            int c=Convert.ToInt32(Console.ReadLine());
            while(c!=6)
            {
                if(banknotes.ContainsKey(c))
                {
                    temp.Add(c);
                    c=Convert.ToInt32(Console.ReadLine());
                }
            }
        }
        static int SumList(List<int>temp)
        {
            int sum = 0;
            for(int i=0;i<temp.Count;i++)
            {
                sum+=temp[i];
            }
            return sum;
        }
    }
    public class BankUser
    {
        public string UserName { get; set; }
        protected int UserAccount=0; //деньги на карточке
        protected int GetterAccount; //деньги получателя  на карточке
        public int UserBanknotes { get; set; } //бумажные деньги на руках
        public BankUser(string user,int money)
        {
            UserName = user;
            UserBanknotes = money;
        }
    }
}
