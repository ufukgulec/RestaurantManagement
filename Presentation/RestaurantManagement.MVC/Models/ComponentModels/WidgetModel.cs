namespace RestaurantManagement.MVC.Models.ComponentModels
{
    public class WidgetModel
    {
        public string? widgetName;
        public string? widgetText;
        public string? widgetIcon;
        public string? widgetLink;
        private List<ListEntity>? _listEntities;
        private List<ListEntity>? _actions;

        public List<ListEntity> widgetEntities => _listEntities ?? (_listEntities = new List<ListEntity>());
        public List<ListEntity> widgetActions => _actions ?? (_actions = new List<ListEntity>());
    }
}
