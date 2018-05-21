using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SQLitePCL;

namespace BudgetBuddy
{
    public class MainViewModel
    {
        private Category _oldCategory;
        public ObservableCollection<Category> Products { get; set; }

        public MainViewModel()
        {
            Products = new ObservableCollection<Category>
            {
                new Category
                {
                    Title = "Vaste Lasten",
                    IsVisible = false,
                    Background = "Aquamarine"
                },
                new Category
                {
                    Title = "Kleding",
                    IsVisible = false,
                    Background = "red"
                },
                new Category
                {
                    Title = "Eten",
                    IsVisible = false,
                    Background = "Yellow"
                },
                new Category
                {
                    Title = "Boodschappen",
                    IsVisible = false,
                    Background = "Green"
                }

            };
        }

        internal void HideOrShowProduct(Category category)
        {
            if (_oldCategory == category)
            {
                category.IsVisible = !category.IsVisible;
                UpdateProducts(category);
            }
            else
            {
                if (_oldCategory != null)
                {
                    _oldCategory.IsVisible = false;
                    UpdateProducts(_oldCategory);
                }

                category.IsVisible = true;
                UpdateProducts(category);
            }

            _oldCategory = category;
        }

        private void UpdateProducts(Category category)
        {
            var index = Products.IndexOf(category);
            Products.Remove(category);
            Products.Insert(index, category);
        }
    }
}
