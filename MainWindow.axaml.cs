using System.Threading.Tasks;
using System;
using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;

namespace Lab3_2sem_3course;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Panel_PointerEnter(object sender, PointerEventArgs e)
    {
        if (sender is StackPanel panel)
        {
           
            var shadow = panel.Effect as DropShadowEffect;

            await Task.WhenAll(
                AnimationUtils.AnimateDouble(panel, ScaleTransform.ScaleXProperty, 1.2, 1000),
                AnimationUtils.AnimateDouble(panel, ScaleTransform.ScaleYProperty, 1.2, 1000),
                AnimationUtils.AnimateDouble(shadow, DropShadowEffect.OpacityProperty, 0.6, 1000)
            );

            // Анимация размытия
            if (panel.Children[0] is ImageWithArrowsUserControl control)
            {
                await AnimationUtils.AnimateDouble(
                    control.Effect as BlurEffect,
                    BlurEffect.RadiusProperty,
                    0,
                    1000
                );
            }
        }
    }

    private async void Panel_PointerLeave(object sender, PointerEventArgs e)
    {
        if (sender is StackPanel panel)
        {
            // Анимация масштаба
            var shadow = panel.Effect as DropShadowEffect;

            await Task.WhenAll(
                AnimationUtils.AnimateDouble(panel, ScaleTransform.ScaleXProperty, 1, 2000),
                AnimationUtils.AnimateDouble(panel, ScaleTransform.ScaleYProperty, 1, 2000),
                AnimationUtils.AnimateDouble(shadow, DropShadowEffect.OpacityProperty, 0, 1000)
            );

            // Анимация размытия
            if (panel.Children[0] is ImageWithArrowsUserControl control)
            {
                await AnimationUtils.AnimateDouble(
                    control.Effect as BlurEffect,
                    BlurEffect.RadiusProperty,
                    3,
                    1000
                );
            }
        }
    }
}

public static class AnimationUtils
{
    public static async Task AnimateDouble(
        this Animatable target,
        AvaloniaProperty property,
        double to,
        int duration
        )
    {
        var animation = new Animation
        {
            Duration = TimeSpan.FromMilliseconds(duration),
            Children =
                {
                    new KeyFrame
                    {
                        Cue = new Cue(1d),
                        Setters = { new Setter(property, to) }
                    }
                }
        };

        await animation.RunAsync(target);
    }

}