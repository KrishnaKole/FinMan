using FinMan.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FinMan
{
    public partial class MainPage : ContentPage
    {
        readonly IList<Book> books = new ObservableCollection<Book>();
        readonly BookManager manager = new BookManager();

        readonly AccountClient client = new AccountClient();
        readonly IList<Account> accounts = new ObservableCollection<Account>();

        public MainPage()
        {
            BindingContext = accounts;
            InitializeComponent();
        }

        async void OnRefresh(object sender, EventArgs e)
        {
            var customer = await client.GetAll("24057788035");

            foreach (Account account in customer.accounts)
            {
                accounts.Add(account);
            }
        }

        async void OnAddNewBook(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new AddEditBookPage(manager, books));
        }

        async void OnEditBook(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(
                new AddEditBookPage(manager, books, (Book)e.Item));
        }

        async void OnDeleteBook(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Book book = item.CommandParameter as Book;
            if (book != null)
            {
                if (await this.DisplayAlert("Delete Book?",
                    "Are you sure you want to delete the book '"
                        + book.Title + "'?", "Yes", "Cancel") == true)
                {
                    await manager.Delete(book.ISBN);
                    books.Remove(book);
                }
            }
        }
    }
}
