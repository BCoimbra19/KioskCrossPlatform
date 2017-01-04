using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace migrosTabletKioke.Droid
{
    [Activity(Label = "Result")]
    public class Result : Activity
    {
        VideoView videoView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Result);
            videoView = FindViewById<VideoView>(Resource.Id.videoView1);

            string text = Intent.GetStringExtra("result") ?? "Data not available";

            if (text == "WIN")
            {
                var uri = Android.Net.Uri.Parse("android.resource://" + Application.PackageName + "/" + Resource.Drawable.WIN);
                videoView.SetVideoURI(uri);
                videoView.Start();
            }
            else
            {
                var uri = Android.Net.Uri.Parse("android.resource://" + Application.PackageName + "/" + Resource.Drawable.LOSE1);
                videoView.SetVideoURI(uri);
                videoView.Start();
            }




        }
    }
}