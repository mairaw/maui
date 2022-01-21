﻿using Android.Views;
using AndroidX.RecyclerView.Widget;
using Microsoft.Maui.Graphics;

namespace Microsoft.Maui.Controls.Handlers.Items
{
	public partial class CarouselViewHandler : ItemsViewHandler<CarouselView>
	{
		double _widthConstraint;
		double _heightConstraint;

		protected override IItemsLayout GetItemsLayout() => VirtualView.ItemsLayout;

		protected override ItemsViewAdapter<CarouselView, IItemsViewSource> CreateAdapter() 
			=> new CarouselViewAdapter<CarouselView, IItemsViewSource>(VirtualView, (view, context) => new SizedItemContentView(Context, GetItemWidth, GetItemHeight));

		protected override RecyclerView CreateNativeView() 
			=> new MauiCarouselRecyclerView(Context, GetItemsLayout, CreateAdapter);

		public static void MapCurrentItem(CarouselViewHandler handler, CarouselView carouselView)
		{
			(handler.NativeView as IMauiCarouselRecyclerView).UpdateFromCurrentItem();
		}

		public static void MapPosition(CarouselViewHandler handler, CarouselView carouselView)
		{
			(handler.NativeView as IMauiCarouselRecyclerView).UpdateFromPosition();
		}

		public static void MapIsBounceEnabled(CarouselViewHandler handler, CarouselView carouselView)
		{
			handler.NativeView.OverScrollMode = carouselView?.IsBounceEnabled == true ? OverScrollMode.Always : OverScrollMode.Never;
		}

		public static void MapIsSwipeEnabled(CarouselViewHandler handler, CarouselView carouselView)
		{
			(handler.NativeView as IMauiCarouselRecyclerView).IsSwipeEnabled = carouselView.IsSwipeEnabled;
		}

		public static void MapPeekAreaInsets(CarouselViewHandler handler, CarouselView carouselView)
		{
			(handler.NativeView as IMauiRecyclerView<CarouselView>).UpdateAdapter();
		}

		public static void MapLoop(CarouselViewHandler handler, CarouselView carouselView)
		{
			(handler.NativeView as IMauiRecyclerView<CarouselView>).UpdateAdapter();
		}

		public override Size GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			_widthConstraint = widthConstraint;
			_heightConstraint = heightConstraint;
			return base.GetDesiredSize(widthConstraint, heightConstraint);
		}

		int GetItemWidth()
		{
			var itemWidth = (int)Context?.ToPixels(_widthConstraint);

			if ((NativeView as IMauiRecyclerView<CarouselView>)?.ItemsLayout is LinearItemsLayout listItemsLayout && listItemsLayout.Orientation == ItemsLayoutOrientation.Horizontal)
				itemWidth = (int)(NativeView.Width - Context?.ToPixels(VirtualView.PeekAreaInsets.Left) - Context?.ToPixels(VirtualView.PeekAreaInsets.Right) - Context?.ToPixels(listItemsLayout.ItemSpacing));

			return itemWidth;
		}

		int GetItemHeight()
		{
			var itemHeight = (int)Context?.ToPixels(_heightConstraint);

			if ((NativeView as IMauiRecyclerView<CarouselView>)?.ItemsLayout is LinearItemsLayout listItemsLayout && listItemsLayout.Orientation == ItemsLayoutOrientation.Vertical)
				itemHeight = (int)(NativeView.Height - Context?.ToPixels(VirtualView.PeekAreaInsets.Top) - Context?.ToPixels(VirtualView.PeekAreaInsets.Bottom) - Context?.ToPixels(listItemsLayout.ItemSpacing));

			return itemHeight;
		}
	}
}