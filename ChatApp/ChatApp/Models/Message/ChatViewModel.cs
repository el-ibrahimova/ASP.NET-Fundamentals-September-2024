namespace ChatApp.Models.Message
{
    public class ChatViewModel
    {
        public MessageViewModel CurrentMessage { get; set; } = null!;
        public List<MessageViewModel> Messages { get; set; } = null!;

        // it isn't good practice to store the data in variable like List<>, because in every time we close the app, the massages will be deleted.
        // we must save them in database
    }
}
