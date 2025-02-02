// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Windows.UI.Core;

namespace CommunityToolkit.Labs.WinUI.SizerBaseLocal;

/// <inheritdoc cref="FrameworkElementExtensions"/>
public static partial class FrameworkElementExtensions
{
    private static readonly object _cursorLock = new object();
    private static readonly CoreCursor _defaultCursor = new CoreCursor(CoreCursorType.Arrow, 1);
    private static readonly Dictionary<CoreCursorType, CoreCursor> _cursors =
        new Dictionary<CoreCursorType, CoreCursor> { { CoreCursorType.Arrow, _defaultCursor } };

    /// <summary>
    /// Dependency property for specifying the target <see cref="CoreCursorType"/> to be shown
    /// over the target <see cref="FrameworkElement"/>.
    /// </summary>
    public static readonly DependencyProperty CursorProperty =
        DependencyProperty.RegisterAttached("Cursor", typeof(CoreCursorType), typeof(FrameworkElementExtensions), new PropertyMetadata(CoreCursorType.Arrow, CursorChanged));

    /// <summary>
    /// Set the target <see cref="CoreCursorType"/>.
    /// </summary>
    /// <param name="element">Object where the selector cursor type should be shown.</param>
    /// <param name="value">Target cursor type value.</param>
    public static void SetCursor(FrameworkElement element, CoreCursorType value)
    {
        element.SetValue(CursorProperty, value);
    }

    /// <summary>
    /// Get the current <see cref="CoreCursorType"/>.
    /// </summary>
    /// <param name="element">Object where the selector cursor type should be shown.</param>
    /// <returns>Cursor type set on target element.</returns>
    public static CoreCursorType GetCursor(FrameworkElement element)
    {
        return (CoreCursorType)element.GetValue(CursorProperty);
    }

    private static void CursorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // TODO: How do we want to indicate this isn't supported on the WinAppSDK?
#if !WINAPPSDK
        var element = d as FrameworkElement;
        if (element == null)
        {
            throw new NullReferenceException(nameof(element));
        }

        var value = (CoreCursorType)e.NewValue;

        // lock ensures CoreCursor creation and event handlers attachment/detachment is atomic
        lock (_cursorLock)
        {
            if (!_cursors.ContainsKey(value))
            {
                _cursors[value] = new CoreCursor(value, 1);
            }

            // make sure event handlers are not attached twice to element
            element.PointerEntered -= Element_PointerEntered;
            element.PointerEntered += Element_PointerEntered;
            element.PointerExited -= Element_PointerExited;
            element.PointerExited += Element_PointerExited;
            element.Unloaded -= ElementOnUnloaded;
            element.Unloaded += ElementOnUnloaded;
        }
#endif
    }

#if !WINAPPSDK
    private static void Element_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        // TODO: [UNO] Only supported on certain platforms
        // See PointerCursor here: https://github.com/unoplatform/uno/blob/3fe3862b270b99dbec4d830b547942af61b1a1d9/src/Uno.UWP/UI/Core/CoreWindow.cs#L71-L77
#if NETFX_CORE || WASM || __MACOS__ || __SKIA__
        CoreCursorType cursor = GetCursor((FrameworkElement)sender);
        Window.Current.CoreWindow.PointerCursor = _cursors[cursor];
#endif
    }

    private static void Element_PointerExited(object sender, PointerRoutedEventArgs e)
    {
#if NETFX_CORE || WASM || __MACOS__ || __SKIA__
        // when exiting change the cursor to the target Mouse.Cursor value of the new element
        CoreCursor cursor;
        if (sender != e.OriginalSource && e.OriginalSource is FrameworkElement newElement)
        {
            cursor = _cursors[GetCursor(newElement)];
        }
        else
        {
            cursor = _defaultCursor;
        }

        Window.Current.CoreWindow.PointerCursor = cursor;
#endif
    }

    private static void ElementOnUnloaded(object sender, RoutedEventArgs routedEventArgs)
    {
#if NETFX_CORE || __WASM__ || __MACOS__ || __SKIA__
        // when the element is programatically unloaded, reset the cursor back to default
        // this is necessary when click triggers immediate change in layout and PointerExited is not called
        Window.Current.CoreWindow.PointerCursor = _defaultCursor;
#endif
    }
#endif
}
