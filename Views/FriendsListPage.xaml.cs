using MVVM.ViewModels;
namespace MVVM.Views;
public partial class FriendsListPage : ContentPage
{
	public FriendsListPage()
	{
		InitializeComponent();
		BindingContext = new FriendsListViewModel { Navigation = Navigation };
	}
}