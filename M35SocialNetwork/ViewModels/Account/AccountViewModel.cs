using App.Data.Models;

namespace M35SocialNetwork.ViewModels.Account
{
    public class AccountViewModel
    {
        public User User { get; private set; }

        public IEnumerable<Friend> Friends { get; set; }
        public AccountViewModel(User user)
        {
            User = user;
        }

    }
}
