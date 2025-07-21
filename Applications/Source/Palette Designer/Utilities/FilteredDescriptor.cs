#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 */
#endregion

using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System;

namespace PaletteDesigner
{
    /// <summary>
    /// Provides a filtered view of an object's properties for display in a <see cref="PropertyGrid"/>,
    /// by implementing <see cref="ICustomTypeDescriptor"/> and omitting any property whose name
    /// exists in the supplied <c>hide</c> collection.
    /// </summary>
    class FilteredDescriptor : ICustomTypeDescriptor
    {
        // Usage example:
        // var ct        = _palette.ColorTable;
        // var wrappedCt = new FilteredDescriptor(ct, new[] { "Palette" }, "Color Table");
        // propertyGridKCT.SelectedObject = wrappedCt;
        // propertyGridKCT.Refresh();

        private readonly object _instance;
        private readonly HashSet<string> _hidden;
        private readonly string _uncategorizedCategoryName;

        public FilteredDescriptor(object instance, IEnumerable<string> hide, string uncategorizedCategoryName)
        {
            _instance = instance;
            _hidden   = new HashSet<string>(hide);
            _uncategorizedCategoryName = uncategorizedCategoryName;
        }

        // delegate most calls straight through
        public AttributeCollection GetAttributes()          => TypeDescriptor.GetAttributes(_instance, true);
        public string              GetClassName()           => TypeDescriptor.GetClassName(_instance, true);
        public string              GetComponentName()       => TypeDescriptor.GetComponentName(_instance, true);
        public TypeConverter       GetConverter()           => TypeDescriptor.GetConverter(_instance, true);
        public EventDescriptor     GetDefaultEvent()        => TypeDescriptor.GetDefaultEvent(_instance, true);
        public PropertyDescriptor  GetDefaultProperty()     => TypeDescriptor.GetDefaultProperty(_instance, true);
        public object              GetPropertyOwner(PropertyDescriptor pd) => _instance;
        public EventDescriptorCollection GetEvents(Attribute[] attr) => TypeDescriptor.GetEvents(_instance, attr, true);
        public EventDescriptorCollection GetEvents()        => TypeDescriptor.GetEvents(_instance, true);
        public object GetEditor(Type editorBaseType)        => TypeDescriptor.GetEditor(_instance, editorBaseType, true);

        // important part: return a filtered list
        public PropertyDescriptorCollection GetProperties() => GetProperties(Array.Empty<Attribute>());
        public PropertyDescriptorCollection GetProperties(Attribute[] attr)
        {
            // Bypass our custom provider by querying the type, not the instance, to avoid recursion.
            var original = TypeDescriptor.GetProperties(_instance.GetType(), attr)
                                         .Cast<PropertyDescriptor>()
                                         .Where(p => !_hidden.Contains(p.Name)); // remove unwanted.

            // Ensure uncategorised properties appear under a friendly heading rather than the default
            // "Misc" (which displays as "Sonstiges" on German systems).
            var transformed = new List<PropertyDescriptor>();
            foreach (var pd in original)
            {
                var catAttr = (CategoryAttribute?)pd.Attributes[typeof(CategoryAttribute)];
                // The PropertyGrid uses CategoryAttribute.Default when no category is specified.
                // Substitute it with a custom category name so the group shows as "Color Table" instead of
                // the localised default.
                if (catAttr is null || catAttr == CategoryAttribute.Default ||
                    string.Equals(catAttr.Category, CategoryAttribute.Default.Category, StringComparison.Ordinal))
                {
                    // Build a new attribute list cloning the existing set but replacing the category.
                    var attrs = pd.Attributes.Cast<Attribute>()
                                              .Where(a => a.GetType() != typeof(CategoryAttribute))
                                              .Append(new CategoryAttribute(_uncategorizedCategoryName))
                                              .ToArray();

                    transformed.Add(TypeDescriptor.CreateProperty(pd.ComponentType, pd, attrs));
                }
                else
                {
                    transformed.Add(pd);
                }
            }

            return new PropertyDescriptorCollection(transformed.ToArray(), true);
        }
    }

    /// <summary>
    /// Registers a <see cref="TypeDescriptionProvider"/> that returns a <see cref="FilteredDescriptor"/>
    /// for a specific object instance, thereby hiding the given property names from the
    /// runtime type descriptor seen by components such as the <see cref="PropertyGrid"/>.
    /// </summary>
    class FilteredProvider : TypeDescriptionProvider
    {
        private readonly TypeDescriptionProvider _baseProvider = TypeDescriptor.GetProvider(typeof(object));
        private readonly IEnumerable<string> _hidden;
        private readonly string _uncategorizedCategoryName;

        public FilteredProvider(IEnumerable<string> hide, string uncategorizedCategoryName)
        {
            _hidden = hide;
            _uncategorizedCategoryName = uncategorizedCategoryName;
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
            => instance is null
                ? base.GetTypeDescriptor(objectType, instance)
                : new FilteredDescriptor(instance, _hidden, _uncategorizedCategoryName);
    }
}