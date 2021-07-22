namespace webtest1.Options
{
    public class DALOptions
    {
            public const string DAL = "DAL";

            public virtual string GetValue(string key)
            {
                switch(key)
                {
                    case "Customer": return Customer;
                    case "Category": return Category;
                    case "Employee": return Employee;
                    case "Product": return Product;
                    default: return "";
                }
            }

            public virtual string About { get; set; }
            public virtual string Customer { get; set; }
            public virtual string Category { get; set; }
            public virtual string Employee { get; set; }
            public virtual string Product { get; set; }
    }
}