using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Networking.Packets;

namespace ConquerServer_Basic.Networking.Packet_Handling
{
    public enum WeatherType
    {
        None = 1,
        Rain = 2,
        Snow = 3,
        RainWind = 4,
        AutumnLeaves = 5,
        CherryBlossomPetals = 7,
        CherryBlossomPetalsWind = 8,
        BlowingCotten = 9,
        Atoms = 10
    }
    public static class Weather
    {
        public static DateTime NextChange = new DateTime();
        public static uint Intensity;
        public static uint Direction;
        public static uint Appearence;
        private static WeatherType _CurrentWeather;

        public static WeatherType CurrentWeather
        {
            get
            {
                return _CurrentWeather;
            }
            set
            {
                _CurrentWeather = value;

                foreach (GameClient Client in Kernel.GamePool.Values)
                {
                    WeatherPacket Weather = new WeatherPacket(true);
                    Weather.Appearance = Appearence;
                    Weather.Direction = Direction;
                    Weather.Intensity = Intensity;
                    Weather.WeatherType = (byte)value;
                    Weather.Send(Client);
                }
            }
        }
    }
}
