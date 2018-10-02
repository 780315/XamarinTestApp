using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;
using Android.Graphics;
using Android.Util;

namespace CSmobile.Droid
{
    [Activity(Label = "CSmobile", Icon = "@mipmap/icon", Theme = "@style/FullScreenTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ISurfaceHolderCallback, MediaPlayer.IOnPreparedListener
    {

        private SurfaceView surfaceView;
        private ISurfaceHolder surfaceHolder;
        private MediaPlayer mediaPlayer;
        private const string VIDEO_PATH = "/Assets/cut-intro.mp4"; //local path not working, with url link scale needs to be fixed.

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            surfaceView = FindViewById<SurfaceView>(Resource.Id.surfaceView);
            surfaceHolder = surfaceView.Holder;
            surfaceHolder.AddCallback(this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
            
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            mediaPlayer = new MediaPlayer();
            mediaPlayer.SetDisplay(surfaceHolder);
            try
            {
                mediaPlayer.SetDataSource(VIDEO_PATH);
                mediaPlayer.Prepare();
                mediaPlayer.SetOnPreparedListener(this);

            }
            catch (Exception ex)
            {
                Log.Error("ERROR", ex.Message);
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            
        }

        public void OnPrepared(MediaPlayer mp)
        {
            mediaPlayer.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
            ReleaseMediaPlayer();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ReleaseMediaPlayer();
        }

        private void ReleaseMediaPlayer()
        {
            if(mediaPlayer != null)
            {
                mediaPlayer.Release();
                mediaPlayer = null;
            }
        }
    }
}

