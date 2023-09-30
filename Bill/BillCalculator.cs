using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bill
{
    public delegate IEnumerable<int> CalculateAmount();
    public class BillCalculator
    {
        public Bill bill { get; set; }
        public BillCalculator(Bill bill)
        {
            this.bill = bill;
        }
        // 傳入參數: 帳單總金額, 小費比率, 總用餐人數
        // 回傳值: IEnumerable<int> 列出每一個人應分攤多少錢
        public IEnumerable<int> 最後一個人多出一點錢()
        {
            List<int> result = new List<int>();
            var totalAmount = Convert.ToInt32(bill.TotalAmount * (1 + (bill.TipRate / 100)));
            var n1 = Convert.ToInt32(totalAmount / bill.NumberOfPeople);
            var n2 = Convert.ToInt32(totalAmount % bill.NumberOfPeople);

            for (int i = 0; i < bill.NumberOfPeople - 1; i++) result.Add(n1);
            result.Add((n1 + n2));

            return result.AsEnumerable();
        }
        public IEnumerable<int> 前面N個人多出一元()
        {
            List<int> result = new List<int>();
            var totalAmount = Convert.ToInt32(bill.TotalAmount * (1 + (bill.TipRate / 100)));
            var n1 = Convert.ToInt32(totalAmount / bill.NumberOfPeople);
            var n2 = Convert.ToInt32(totalAmount % bill.NumberOfPeople);
            for (int i = 0; i < bill.NumberOfPeople; i++) result.Add(n1);

            if (n2 != 0)
            {
                for (int i = 0; i < bill.NumberOfPeople; i++)
                {
                    if (n2 == 0) break;
                    result[i] += 1;
                    n2--;
                }
            }

            return result.AsEnumerable();
        }

        public string GetResult(CalculateAmount calculateAmount)
        {
            StringBuilder resultString = new StringBuilder();
            var billCalculator = calculateAmount();

            for (int i = 0; i < bill.NumberOfPeople; i++)
            {
                resultString.AppendLine($"第{i + 1}個人，應付{billCalculator.ElementAt(i)}元");
            }

            return resultString.ToString();
        }
    }

    public class Bill
    {
        public int TotalAmount { get; set; }
        public decimal TipRate { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
