using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class V_FinanceChart
    {
        [Key]
        public decimal Amount { set; get; }
        public string Date { set; get; }
        public int Register_ID { set; get; }
    }
}
