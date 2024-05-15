using MVVM.ViewModels;

namespace MVVM.Views;

public partial class FriendPage : ContentPage
{
	public FriendViewModel ViewModel { get; private set; }
	public FriendPage(FriendViewModel vm)
	{
		InitializeComponent();
		ViewModel = vm;
		BindingContext = ViewModel;
	}
}