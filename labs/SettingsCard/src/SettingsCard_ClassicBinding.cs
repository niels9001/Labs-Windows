// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CommunityToolkit.Labs.WinUI;

/// <summary>
/// An example templated control.
/// </summary>
//[TemplatePart(Name = nameof(PART_HelloWorld), Type = typeof(TextBlock))]
//private const string NormalState = "Normal";
//private const string PointerOverState = "PointerOver";
//private const string PressedState = "Pressed";
//private const string DisabledState = "Disabled";
public partial class SettingsCard_ClassicBinding : ButtonBase
{
    /// <summary>
    /// Creates a new instance of the <see cref="SettingsCard_ClassicBinding"/> class.
    /// </summary>
    public SettingsCard_ClassicBinding()
    {
        this.DefaultStyleKey = typeof(SettingsCard_ClassicBinding);
    }

    /// <summary>
    /// The primary text block that displays "Hello world".
    /// </summary>
    protected TextBlock? PART_HelloWorld { get; private set; }

    /// <inheritdoc />
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        //// Detach all attached events when a new template is applied.
        //if (PART_HelloWorld is not null)
        //{
        //    PART_HelloWorld.PointerEntered -= Element_PointerEntered;
        //}

        //// Attach events when the template is applied and the control is loaded.
        //PART_HelloWorld = GetTemplateChild(nameof(PART_HelloWorld)) as TextBlock;
        
        //if (PART_HelloWorld is not null)
        //{
        //    PART_HelloWorld.PointerEntered += Element_PointerEntered;
        //}
    }

    /// <summary>
    /// The backing <see cref="DependencyProperty"/> for the <see cref="ItemPadding"/> property.
    /// </summary>
    public static readonly DependencyProperty ItemPaddingProperty = DependencyProperty.Register(
        nameof(ItemPadding),
        typeof(Thickness),
        typeof(SettingsCard_ClassicBinding),
        new PropertyMetadata(defaultValue: new Thickness(0)));

    /// <summary>
    /// The backing <see cref="DependencyProperty"/> for the <see cref="Title"/> property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(SettingsCard_ClassicBinding),
        new PropertyMetadata(defaultValue: string.Empty, (d, e) => ((SettingsCard_ClassicBinding)d).OnTitlePropertyChanged((string)e.OldValue, (string)e.NewValue)));

    /// <summary>
    /// The backing <see cref="DependencyProperty"/> for the <see cref="Description"/> property.
    /// </summary>
    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
        nameof(Description),
        typeof(string),
        typeof(SettingsCard_ClassicBinding),
        new PropertyMetadata(defaultValue: string.Empty, (d, e) => ((SettingsCard_ClassicBinding)d).OnDescriptionPropertyChanged((string)e.OldValue, (string)e.NewValue)));

    /// <summary>
    /// Gets or sets an example string. A basic DependencyProperty example.
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets an example string. A basic Description example.
    /// </summary>
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets a padding for an item. A basic DependencyProperty example.
    /// </summary>
    public Thickness ItemPadding
    {
        get => (Thickness)GetValue(ItemPaddingProperty);
        set => SetValue(ItemPaddingProperty, value);
    }

    protected virtual void OnTitlePropertyChanged(string oldValue, string newValue)
    {
        // Do something with the changed value.
    }

    protected virtual void OnDescriptionPropertyChanged(string oldValue, string newValue)
    {
        // Do something with the changed value.
    }

    public void Element_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        if (sender is TextBlock text)
        {
            text.Opacity = 1;
        }
    }

    //protected override void OnPointerEntered(PointerRoutedEventArgs e)
    //{
    //    base.OnPointerEntered(e);
    //    VisualStateManager.GoToState(this, PointerOverState, true);
    //}

    //protected override void OnPointerExited(PointerRoutedEventArgs e)
    //{
    //    base.OnPointerExited(e);
    //    VisualStateManager.GoToState(this, NormalState, true);
    //}
    //protected override void OnPointerPressed(PointerRoutedEventArgs e)
    //{
    //    base.OnPointerPressed(e);
    //    VisualStateManager.GoToState(this, PressedState, true);
    //}

}
