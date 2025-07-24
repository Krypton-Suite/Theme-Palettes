#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner.Utilities;

public static class PropertyGridHelper
{
    /// <summary>
    /// Finds the internal grid entry matching the given property path and sets its value,
    /// notifying the PropertyGridView to update that single entry in-place.
    /// </summary>
    /// <param name="propertyGrid">The PropertyGrid to update.</param>
    /// <param name="propertyPath">Dot-separated path (e.g. "ButtonStyles.ButtonNormal.Back1").</param>
    /// <param name="newValue">The new value to assign.</param>
    public static void UpdatePropertyEntry(PropertyGrid propertyGrid, string propertyPath, object newValue)
    {
        // Only use the leaf property name for matching
        string propertyName = propertyPath.Split('.').Last();

        // Access the private gridView field on PropertyGrid
        var gridViewField = typeof(PropertyGrid).GetField("gridView", BindingFlags.Instance | BindingFlags.NonPublic);
        if (gridViewField?.GetValue(propertyGrid) is object gridView)
        {
            // Invoke the internal GetAllGridEntries method
            var getEntries = gridView.GetType()
                                     .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                                     .FirstOrDefault(mi => mi.Name == "GetAllGridEntries" &&
                                                     mi.GetParameters().Length == 0);
            if (getEntries?.Invoke(gridView, null) is Array entries)
            {
                foreach (var entry in entries)
                {
                    // Each GridEntry has a non-public 'propertyDescriptor' field
                    var pdField = entry.GetType().GetField("propertyDescriptor", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (pdField?.GetValue(entry) is PropertyDescriptor pd && pd.Name == propertyName)
                    {
                        // Set the new value on the underlying object
                        pd.SetValue(propertyGrid.SelectedObject, newValue);

                        // Notify the gridView that this entry changed
                        var onChanged = gridView.GetType()
                                        .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                                        .FirstOrDefault(mi => mi.Name == "OnPropertyValueChanged" &&
                                                        mi.GetParameters().Length == 2);
                        if (onChanged != null)
                            onChanged.Invoke(gridView, new object[] { entry, true });
                        break;
                    }
                }
            }
        }
    }
}