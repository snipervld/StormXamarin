using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using Storm.Mvvm.Interfaces;
using System;

namespace Storm.Mvvm.Components
{
    public class BindableListView : ListView
    {
        public event EventHandler CurrentItemChanged;

        public object CurrentItem
        {
            get { return SelectedItem; }
            set
            {
                int position = -1;
                var adapter = Adapter as ISearchableAdapter;

                if (adapter != null)
                {
                    position = adapter.IndexOf(value);
                }

                if (position >= 0)
                {
                    if (SelectedItemPosition != position)
                    {
                        SetSelection(position);
                    }
                }
            }
        }

        protected BindableListView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public BindableListView(Context context) : base(context)
        {
            Initialize();
        }

        public BindableListView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public BindableListView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        private void Initialize()
        {
            ItemSelected += (sender, args) => OnCurrentItemChanged();
        }

        protected void OnCurrentItemChanged()
        {
            EventHandler handler = CurrentItemChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
