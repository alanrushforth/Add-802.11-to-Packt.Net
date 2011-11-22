﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PacketDotNet.Utils;

namespace PacketDotNet
{
    namespace Ieee80211
    {
        #region Action Category Enums


        //The following enums define the category and type of action. At present these are 
        //not handled and parsed but they are left here for future reference as tracking them down 
        //was not that easy

        //enum ActionCategory
        //{
        //    SpectrumManagement = 0x0,
        //    Qos = 0x1,
        //    Dls = 0x2,
        //    BlockAck = 0x3,
        //    VendorSpecific = 0x127
        //}

        //enum SpectrumManagementAction
        //{
        //    MeasurementRequest = 0x0,
        //    MeasurementReport = 0x1,
        //    TpcRequest = 0x2,
        //    TpcReport = 0x3,
        //    ChannelSwitchAnnouncement = 0x4
        //}

        //enum QosAction
        //{
        //    TrafficSpecificationRequest = 0x0,
        //    TrafficSpecificationResponse = 0x1,
        //    TrafficSpecificationDelete = 0x2,
        //    Schedule = 0x3
        //}

        //enum DlsAction
        //{
        //    DlsRequest = 0x0,
        //    DlsResponse = 0x1,
        //    DlsTeardown = 0x2
        //}

        //enum BlockAcknowledgmentActions
        //{
        //    BlockAcknowledgmentRequest = 0x0,
        //    BlockAcknowledgmentResponse = 0x1,
        //    BlockAcknowledgmentDelete = 0x2
        //}

        #endregion

        /// <summary>
        /// Format of an 802.11 management action frame. These frames are used by the 802.11e (QoS) and 802.11n standards to request actions of stations.
        /// </summary>
        public class ActionFrame : ManagementFrame
        {
            public override int FrameSize
            {
                get
                {
                    return (MacFields.FrameControlLength +
                        MacFields.DurationIDLength +
                        (MacFields.AddressLength * 3) +
                        MacFields.SequenceControlLength);
                }
            }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="bas">
            /// A <see cref="ByteArraySegment"/>
            /// </param>
            public ActionFrame(ByteArraySegment bas)
            {
                header = new ByteArraySegment(bas);

                FrameControl = new FrameControlField(FrameControlBytes);
                Duration = new DurationField(DurationBytes);
                SequenceControl = new SequenceControlField(SequenceControlBytes);

                header.Length = FrameSize;
                int payloadLength = header.BytesLength - (header.Offset + header.Length) - MacFields.FrameCheckSequenceLength;
                payloadPacketOrData.TheByteArraySegment = header.EncapsulatedBytes(payloadLength);
            }

            /// <summary>
            /// ToString() override
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/>
            /// </returns>
            public override string ToString()
            {
                return string.Format("FrameControl {0}, FrameCheckSequence {1}, [ActionFrame RA {2} TA {3} BSSID {4}]",
                                     FrameControl.ToString(),
                                     FrameCheckSequence,
                                     DestinationAddress.ToString(),
                                     SourceAddress.ToString(),
                                     BssId.ToString());
            }
        }

    }
}