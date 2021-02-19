﻿using Foundation;
using UIKit;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public static class ButtonExtensions
	{
		public static void UpdateText(this UIButton view, string text)
			=> view.SetTitle(text, UIControlState.Normal);

		public static void UpdateText(this UIButton view, IButton button)
			=> view.UpdateText(button.Text);

		public static void UpdateText(this UIButton view, NSAttributedString text)
			=> view.SetAttributedTitle(text, UIControlState.Normal);

		public static void UpdateTextColor(this UIButton button, Color color, Color defaultColor) =>
			button.SetTitleColor(color.Cleanse(defaultColor).ToNative(), UIControlState.Normal);

		public static void UpdateTextColor(this UIButton nativeButton, IButton button, Color defaultColor) =>
			nativeButton.UpdateTextColor(button.TextColor, defaultColor);

		public static void UpdateTextColor(this UIButton nativeButton, IButton button) =>
			nativeButton.UpdateTextColor(button.TextColor, nativeButton.TitleColor(UIControlState.Normal).ToColor());

		static Color Cleanse(this Color color, Color defaultColor) => color.IsDefault ? defaultColor : color;

	}
}
