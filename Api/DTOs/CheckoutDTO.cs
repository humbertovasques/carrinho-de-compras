namespace Api.DTOs
{
    public class CheckoutDTO
    {
        public List<ItemDTO>? Itens { get; set; }
        public double Frete { get; set; }
        public double Total { get; set; }
    }
}