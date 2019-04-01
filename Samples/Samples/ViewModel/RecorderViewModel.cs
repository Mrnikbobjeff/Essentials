﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Samples.ViewModel
{
    public class RecorderViewModel : BaseViewModel
    {
        public ICommand StartRecording { get; }

        public ICommand StopRecording { get; }

        public RecorderViewModel()
        {
            StartRecording = new Command(async () => await Recorder.RecordAsync());
            StopRecording = new Command(async () => await OnRecordingStopped());
        }

        async Task OnRecordingStopped()
        {
            var record = await Recorder.StopAsync();
            var size = File.ReadAllBytes(record.Filepath).Length;
            await DisplayAlertAsync($"Recording saved in {record.Filepath}, size is {size}");
        }
    }
}
