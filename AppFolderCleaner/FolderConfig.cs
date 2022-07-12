using System;
using System.Configuration;

namespace AppFolderCleaner
{
    public class FolderConfig : ConfigurationElement
    {
        public FolderConfig()
        {

        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("path", IsRequired = true)]
        public string Path
        {
            get { return (string)this["path"]; }
            set { this["path"] = value; }
        }

        [ConfigurationProperty("maxelements", IsRequired = true)]
        public int MaxElements
        {
            get { return (int)this["maxelements"]; }
            set { this["maxelements"] = value; }
        }
    }

    public class FolderConfigCollection : ConfigurationElementCollection
    {
        public FolderConfigCollection()
        {
            FolderConfig myElement = (FolderConfig)CreateNewElement();
            Add(myElement);
        }

        public void Add(FolderConfig customElement)
        {
            BaseAdd(customElement);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            base.BaseAdd(element, false);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderConfig)element).Name;
        }

        public FolderConfig this[int Index]
        {
            get
            {
                return (FolderConfig)BaseGet(Index);
            }
            set
            {
                if (BaseGet(Index) != null)
                {
                    BaseRemoveAt(Index);
                }
                BaseAdd(Index, value);
            }
        }

        new public FolderConfig this[string Name]
        {
            get
            {
                return (FolderConfig)BaseGet(Name);
            }
        }

        public int indexof(FolderConfig element)
        {
            return BaseIndexOf(element);
        }

        public void Remove(FolderConfig url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }

    public class CustomSection : ConfigurationSection
    {
        FolderConfig element;
        public CustomSection()
        {
            element = new FolderConfig();
        }

        [ConfigurationProperty("elements", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(FolderConfigCollection), AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public FolderConfigCollection Elements
        {
            get
            {
                return (FolderConfigCollection)base["elements"];
            }
        }
    }
}
