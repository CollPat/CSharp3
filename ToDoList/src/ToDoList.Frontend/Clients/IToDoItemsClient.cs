using ToDoList.Frontend.Views;

namespace ToDoList.Frontend.Clients
{
    public interface IToDoItemsClient
    {
        public Task<List<ToDoItemView>> ReadItemsAsync();

    }
}
