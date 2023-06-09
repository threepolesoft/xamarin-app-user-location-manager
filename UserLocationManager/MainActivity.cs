using Android.App;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using AndroidX.AppCompat.App;
using System.Collections.Generic;
using System.Linq;
using AndroidX.Core.Location;

namespace UserLocationManager
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ILocationListener
    {
        Button btnStart, btnStop;


        private TextView txtLatitudeStart, txtLongitudeStart, txtLatitudeStop, txtLongitudeStop;

        Location currentLocation;
        LocationManagerCompat locationManager;
        string locationProvider;
        public string TAG
        {
            get;
            private set;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            txtLatitudeStart = FindViewById<TextView>(Resource.Id.txtLatitudeStart);
            txtLongitudeStart = FindViewById<TextView>(Resource.Id.txtLongitudeStart);

            txtLatitudeStop = FindViewById<TextView>(Resource.Id.txtLatitudeStop);
            txtLongitudeStop = FindViewById<TextView>(Resource.Id.txtLongitudeStop);

            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            btnStop = FindViewById<Button>(Resource.Id.btnStop);


            InitializeLocationManager();
        }

        private void InitializeLocationManager()
        {
            locationManager = (LocationManagerCompat)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };

            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);
            if (acceptableLocationProviders.Any())
            {
                locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + locationProvider + ".");
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnLocationChanged(Location location)
        {
            currentLocation = location;
            if (currentLocation == null)
            {
                //Error Message  
            }
            else
            {
                txtLatitudeStart.Text = currentLocation.Latitude.ToString();
                txtLongitudeStart.Text = currentLocation.Longitude.ToString();
            }
        }

        public void OnProviderDisabled(string provider)
        {
            throw new System.NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new System.NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnResume()
        {
            base.OnResume();
            //locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
        }
        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }
    }
}