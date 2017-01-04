using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing.Mobile;
using Android.Preferences;

namespace migrosTabletKioke.Droid
{
    [Activity(Label = "migrosTabletKioke.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class StartUp : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        Button buttonContinue;
        EditText editTextKioskKey;
        EditText editTextEndPoint;
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;
        AlertDialog.Builder alert;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            /*
            prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            if (prefs.GetString("kiosk_key", "NULL") == "NULL" || prefs.GetString("end_point", "NULL") == "NULL")
            {
                SetContentView(Resource.Layout.StartUp);

                buttonContinue = FindViewById<Button>(Resource.Id.buttonContinue);
                editTextKioskKey = FindViewById<EditText>(Resource.Id.editTextKioskKey);
                editTextEndPoint = FindViewById<EditText>(Resource.Id.editTextEndPoint);

                buttonContinue.Click += ButtonContinue_Click;
            }
            else
            {
                StartActivity(typeof(SScreen));
            }*/
        }

        private void ButtonContinue_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(editTextKioskKey.Text))
            {
                alert = new AlertDialog.Builder(this);
                alert.SetTitle("Erro");
                alert.SetMessage("Please Insert a Kiosk Key");
                alert.SetPositiveButton("Ok", (senderAlert, args) =>
                {
                   // Toast.MakeText(this, "", ToastLength.Short).Show();
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            else if (String.IsNullOrEmpty(editTextEndPoint.Text))
            {
                alert = new AlertDialog.Builder(this);
                alert.SetTitle("Error");
                alert.SetMessage("Please Insert an End Point");
                alert.SetPositiveButton("Ok", (senderAlert, args) =>
                {
                    //Toast.MakeText(this, "Deleted!", ToastLength.Short).Show();
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            else
            {
                editor = prefs.Edit();
                editor.PutString("kiosk_key", editTextKioskKey.Text);
                editor.PutString("end_point", editTextEndPoint.Text);
                editor.Apply();

                StartActivity(typeof(SScreen));
            }  
        }
    }
}


