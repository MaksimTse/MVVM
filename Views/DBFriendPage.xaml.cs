using MVVM.Models;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Microsoft.Maui.Controls;

namespace MVVM.Views;

public partial class DBFriendPage : ContentPage
{
	public DBFriendPage()
	{
		InitializeComponent();
        Loaded += DBFriendPage_Loaded;
	}

    private void DBFriendPage_Loaded(object sender, EventArgs e)
    {
        Friend friend = (Friend)BindingContext;
        pilt.Source = ImageSource.FromStream(() => new MemoryStream(friend.Img));
    }

    private void SaveFriend(object sender, EventArgs e)
    {
        Friend friend = (Friend)BindingContext;
        if (!string.IsNullOrEmpty(friend.Name) && !string.IsNullOrEmpty(friend.Email) && !string.IsNullOrEmpty(friend.Phone) && !string.IsNullOrEmpty(friend.Age))
        {
            if (int.TryParse(friend.Age, out int age))
            {
                if (age >= 6 && age <= 100)
                {
                    if (friend.Email.Contains("@gmail.com"))
                    {
                        if (friend.Phone.StartsWith("+372") && friend.Phone.Length == 12)
                        {
                            App.Database.SaveItem(friend);
                            Navigation.PopAsync();
                        }
                        else
                        {
                            DisplayAlert("Viga", "Telefoninumber peab algama +372 ja olema 12 märki pikk", "OK");
                        }
                    }
                    else
                    {
                        DisplayAlert("Viga", "E-posti aadress peab olema @gmail.com domeen", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Viga", "Vanus peab olema vahemikus 6 kuni 100 aastat", "OK");
                }
            }
            else
            {
                DisplayAlert("Viga", "Vanus peab olema number", "OK");
            }
        }
        else
        {
            DisplayAlert("Viga", "Kõik väljad peavad olema täidetud", "OK");
        }
    }


    private void DeleteFriend(object sender, EventArgs e)
    {
        Friend friend = (Friend)BindingContext;
        App.Database.DeleteItem(friend.Id);
        Navigation.PopAsync();
    }

    private void Cancel(object sender, EventArgs e) =>
        Navigation.PopAsync();

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await CrossMedia.Current.Initialize();
        MediaFile new_image = await CrossMedia.Current.PickPhotoAsync();
        Friend friend = (Friend)BindingContext;
        Stream stream = await ((StreamImageSource)ImageSource.FromStream(new_image.GetStream)).Stream(CancellationToken.None);
        byte[] image = new byte[stream.Length];
        stream.Read(image, 0, image.Length);
        friend.Img = image;
        App.Database.SaveItem(friend);
        pilt.Source = ImageSource.FromStream(new_image.GetStream);
    }
}