using RestaurantManagement.UI.Utils.Components.Cards.Models;

namespace RestaurantManagement.UI.Pages.CategoryPages
{
    public partial class CategoryDashboard : BasePage
    {
        public List<DividedCardModel> cards = new List<DividedCardModel>();

        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() =>
            {
                bind_dividedCard();

            });
        }

        private void bind_dividedCard()
        {
            cards = new List<DividedCardModel>()
                {
                    new DividedCardModel(){CardName="Toplam Kategori",Icon="dripicons-briefcase",Number=10},
                    new DividedCardModel(){CardName="Aktif Kategori",Icon="dripicons-checklist",Number=9},
                    new DividedCardModel(){CardName="Pasif Kategori",Icon="dripicons-briefcase",Number=1},
                    new DividedCardModel(){CardName="deneme",Icon="dripicons-checklist",Number=1}
                };
        }
    }
}
