using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using System.Net;
using Android.Widget;
using ZXing.Mobile;
using Android.Webkit;
using Android.Preferences;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace migrosTabletKioke.Droid
{
    [Activity(Label = "SSCREEN")]
    public class SScreen : Activity
    {
        Button buttonStartScan;
        ImageView imageViewlogo;
        WebView webView;
        ISharedPreferences prefs;
        AlertDialog.Builder alert;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SSCREEN);
            prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            buttonStartScan = FindViewById<Button>(Resource.Id.buttonStartScan);
            imageViewlogo = FindViewById<ImageView>(Resource.Id.imageViewlogo);

            buttonStartScan.Click += ButtonStartScan_Click;

            imageViewlogo.SetImageResource(Resource.Drawable.msw_logo);
        }

        private async void ButtonStartScan_Click(object sender, EventArgs e)
        {
            MobileBarcodeScanner.Initialize(Application);
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();

            string url = "http://work.innovagencyhost.com/station/kioskdev/ws/WS_validate.php?code=" + result + "&kiosk_key=" + prefs.GetString("kiosk_key", "NULL");

            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject(content);

                var responseFromServerResult = JObject.Parse(Convert.ToString(Items))["Result"];
                var responseFromServerErrorMessage = JObject.Parse(Convert.ToString(Items))["ERRORMSG"];

                alert = new AlertDialog.Builder(this);
                alert.SetTitle("Informacao");
                alert.SetMessage("Response From Server-> Result " + responseFromServerResult + "\nError Message: " + responseFromServerErrorMessage);
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

                var activityResult = new Intent(this, typeof(Result));
                activityResult.PutExtra("result", Convert.ToString(responseFromServerResult));
                StartActivity(activityResult);
            }
        }
    }
}