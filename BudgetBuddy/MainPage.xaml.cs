using Xamarin.Forms;

namespace BudgetBuddy
{
	public class Recipe 
	{
		public int Id { get; set; }
		public string Name  { get; set; }
	}

	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		void OnAdd(object sender, System.EventArgs e)
		{
		}

		void OnUpdate(object sender, System.EventArgs e)
		{
		}

		void OnDelete(object sender, System.EventArgs e)
		{
		}
	}
}