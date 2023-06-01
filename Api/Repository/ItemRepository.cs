using Api.DTOs;

namespace Api.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly List<ItemDTO> _items;

        public ItemRepository()
        {
            _items = new List<ItemDTO>();
        }

        public void AddItem(ItemDTO item)
        {
            _items.Add(item);
        }

        public List<ItemDTO> GetAllItems()
        {
            return _items.ToList();
        }
    }
}