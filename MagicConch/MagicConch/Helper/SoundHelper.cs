﻿using NAudio.Wave;
using System;
namespace MagicConch.Helper
{
    internal class SoundHelper
    {
        string audioFilePath;

        AudioFileReader audioFile;
        WaveOutEvent outputDevice;
        public SoundHelper(string filePath)
        {
            audioFilePath = filePath;
            audioFile = new AudioFileReader(audioFilePath);
            outputDevice = new WaveOutEvent();

            // 초기 필수
            outputDevice.Init(audioFile);
        }
        public void Play()
        {
            outputDevice.Play();
        }
        public void PlayLooping()
        {
            outputDevice.PlaybackStopped += PlaybackStoppedHandler!;
            outputDevice.Play();
        }
        public void Stop()
        {
            outputDevice.PlaybackStopped -= PlaybackStoppedHandler!;
            outputDevice.Stop();
        }
        public void SetVolume(float volume)
        {
            outputDevice.Volume = volume;
        }
        public void Dispose()
        {
            outputDevice.Dispose();
            audioFile.Dispose();
        }
        public void PlaybackStoppedHandler(object sender, StoppedEventArgs e)
        {
            // 오디오 파일의 위치를 처음으로 재설정
            audioFile.Position = 0;
            outputDevice.Play();
        }
    }
}
