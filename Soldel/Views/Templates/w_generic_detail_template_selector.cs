namespace mupeModel.Views.Templates {
    using NHibernate;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal class w_generic_detail_template_selector : DataTemplateSelector {
        public override DataTemplate SelectTemplate(object item,DependencyObject container) {
            if(item != null) {
                string resourceKey = NHibernateUtil.GetClass(item).Name + "_template";
                FrameworkElement element = (FrameworkElement)container;
                return (element.TryFindResource(resourceKey) as DataTemplate);
            }
            return null;
        }
    }
}
