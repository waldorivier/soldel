using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace mupeModel.Views.Templates {
    class EwaTemplateSelector : DataTemplateSelector {

        public override DataTemplate SelectTemplate(Object item, DependencyObject container) {
            if (item != null) {
                Type type = item.GetType();
                string templateName = type.Name;
                templateName += "_template";
                FrameworkElement element = (FrameworkElement)container;
                return element.TryFindResource(templateName) as DataTemplate;
            }
            return null;
        }
    }
}
