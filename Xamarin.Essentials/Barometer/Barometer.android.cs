﻿using System;
using Android.Hardware;
using Android.Runtime;

namespace Xamarin.Essentials
{
    public static partial class Barometer
    {
        static bool PlatformIsSupported =>
               DefaultBarometer != null;

        static Sensor DefaultBarometer => Platform.SensorManager?.GetDefaultSensor(SensorType.Pressure);

        static Sensor barometer;

        static BarometerListener listener;

        static void PlatformStart()
        {
            listener = new BarometerListener();
            barometer = DefaultBarometer;
            Platform.SensorManager.RegisterListener(listener, barometer, SensorDelay.Normal);
        }

        static void PlatformStop()
        {
            if (listener == null)
                return;

            Platform.SensorManager.UnregisterListener(listener, barometer);
            listener.Dispose();
            listener = null;
        }
    }

    class BarometerListener : Java.Lang.Object, ISensorEventListener, IDisposable
    {
        void ISensorEventListener.OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
        }

        void ISensorEventListener.OnSensorChanged(SensorEvent e)
            => Barometer.OnChanged(new BarometerData(e.Values[0]));
    }
}
