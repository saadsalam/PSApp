using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AppWorks.UI.Infrastructure.Navigation
{
    public class NavigationNode
    {
        public string Name { get; private set; }
        public object Icon { get; set; }
        public Type Type { get; private set; }
        public NavigationNode Parent { get; private set; }
        public IList<NavigationNode> Children { get; private set; }

        public bool IsSelected { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsFirstTimeLoaded { get; set; }

        public NavigationNode()
        {
            Children = new ObservableCollection<NavigationNode>();
        }

        public void AddChild(NavigationNode child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        public NavigationNode AddChild(string name)
        {
            NavigationNode child = new NavigationNode
            {
                Name = name
            };
            AddChild(child);

            return child;
        }

        public NavigationNode AddChild(string name, Type type)
        {
            NavigationNode child = AddChild(name);
            child.Type = type;
            return child;
        }
    }
}
