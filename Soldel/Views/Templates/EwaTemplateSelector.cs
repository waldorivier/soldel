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
                // retourne le type reel de l'objet en particulier si l'objet est de type proxy
                string templateName = NHibernate.NHibernateUtil.GetClass(item).Name;
                templateName += "_template";
                FrameworkElement element = (FrameworkElement)container;
                return element.TryFindResource(templateName) as DataTemplate;
            }
            return null;
        }
    }
}
