using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;
using Android.Graphics;
using Android.Util;
using System.Timers;
using Xamarin.Forms;

namespace CSmobile.Droid
{
    [Activity(Label = "CSmobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity //, ISurfaceHolderCallback, MediaPlayer.IOnPreparedListener
    {
        //Change theme to FullscreenTheme for splash screen
        //private SurfaceView surfaceView;
        //private ISurfaceHolder surfaceHolder;
        //private MediaPlayer mediaPlayer;
        //private const string VIDEO_PATH = "https://www.cs.team/wp-content/themes/csg-theme/assets/video/04.cs.team_BFX01.mp4"; //local path not working, with url link the scale needs to be fixed.

        public override void OnBackPressed()
        {
            MainPage main = new MainPage();
            Xamarin.Forms.Application.Current.MainPage = main;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
           
            base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.Main);
            //surfaceView = FindViewById<SurfaceView>(Resource.Id.surfaceView);
            //surfaceHolder = surfaceView.Holder;
            //surfaceHolder.AddCallback(this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

        }

        //public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        //{

        //}

        //public void SurfaceCreated(ISurfaceHolder holder)
        //{
        //    mediaPlayer = new MediaPlayer();
        //    mediaPlayer.SetDisplay(surfaceHolder);
        //    try
        //    {
        //        mediaPlayer.SetDataSource(VIDEO_PATH);
        //        mediaPlayer.Prepare();
        //        mediaPlayer.SetOnPreparedListener(this);                               
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("ERROR", ex.Message);
        //    }
        //}       

        //public void SurfaceDestroyed(ISurfaceHolder holder)
        //{

        //}

        //public void OnPrepared(MediaPlayer mp)
        //{
        //    mediaPlayer.Start();
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    ReleaseMediaPlayer();
        //}

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    ReleaseMediaPlayer();
        //}

        //private void ReleaseMediaPlayer()
        //{
        //    if (mediaPlayer != null)
        //    {
        //        mediaPlayer.Release();
        //        mediaPlayer = null;
        //    }
        //}

    }
}

