namespace BookStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine? line = lineCollection.Where
                (p => p.ProductId == product.Id).FirstOrDefault();

            if (line == null)
                lineCollection.Add(new CartLine 
                { 
                    ProductId = product.Id, 
                    Name = product.Name,    
                    Quantity = quantity, 
                    Price = product.Price
                });
            else
                line.Quantity += quantity;
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.ProductId == product.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public string Name { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

