using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Interfaces
{
    public interface IPortal
    {
        ushort CurrentMapID { get; set; }
        ushort CurrentX { get; set; }
        ushort CurrentY { get; set; }
        ushort DestinationMapID { get; set; }
        ushort DestinationX { get; set; }
        ushort DestinationY { get; set; }
    }

    public class Portal : IPortal
    {
        private ushort _CurrentMapID;
        private ushort _CurrentX;
        private ushort _CurrentY;
        private ushort _DestinationMapID;
        private ushort _DestinationX;
        private ushort _DestinationY;

        public Portal()
        {
        }

        public Portal(ushort CurrentMapID, ushort CurrentX, ushort CurrentY, ushort DestinationMapID, ushort DestinationX, ushort DestinationY)
        {
            _CurrentMapID = CurrentMapID;
            _CurrentX = CurrentX;
            _CurrentY = CurrentY;
            _DestinationMapID = DestinationMapID;
            _DestinationX = DestinationX;
            _DestinationY = DestinationY;
        }

        public ushort CurrentMapID
        {
            get
            {
                return _CurrentMapID;
            }

            set
            {
                _CurrentMapID = value;
            }
        }
        public ushort CurrentX
        {
            get
            {
                return _CurrentX;
            }

            set
            {
                _CurrentX = value;
            }
        }
        public ushort CurrentY
        {
            get
            {
                return _CurrentY;
            }

            set
            {
                _CurrentY = value;
            }
        }
        public ushort DestinationMapID
        {
            get
            {
                return _DestinationMapID;
            }

            set
            {
                _DestinationMapID = value;
            }
        }
        public ushort DestinationX
        {
            get
            {
                return _DestinationX;
            }

            set
            {
                _DestinationX = value;
            }
        }
        public ushort DestinationY
        {
            get
            {
                return _DestinationY;
            }

            set
            {
                _DestinationY = value;
            }
        }
    }
}
