using Api.DTOs;

namespace Api.Repository
{
    public interface IItemRepository
    {
        void AddItem(ItemDTO item);
        List<ItemDTO> GetAllItems();
    }
}