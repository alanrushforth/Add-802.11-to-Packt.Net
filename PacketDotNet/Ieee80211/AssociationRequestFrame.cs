﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PacketDotNet.Utils;
using MiscUtil.Conversion;

namespace PacketDotNet
{
    namespace Ieee80211
    {
        /// <summary>
        /// Format of an 802.11 management association frame.
        /// </summary>
        public class AssociationRequestFrame : ManagementFrame
        {
            private class AssociationRequestFields
            {
                public readonly static int CapabilityInformationLength = 2;
                public readonly static int ListenIntervalLength = 2;

                public readonly static int CapabilityInformationPosition;
                public readonly static int ListenIntervalPosition;
                public readonly static int InformationElement1Position;

                static AssociationRequestFields()
                {
                    CapabilityInformationPosition = MacFields.SequenceControlPosition + MacFields.SequenceControlLength;
                    ListenIntervalPosition = CapabilityInformationPosition + CapabilityInformationLength;
                    InformationElement1Position = ListenIntervalPosition + ListenIntervalLength;
                }
            }

            /// <summary>
            /// Frame control bytes are the first two bytes of the frame
            /// </summary>
            public UInt16 CapabilityInformationBytes
            {
                get
                {
                    return EndianBitConverter.Little.ToUInt16(header.Bytes,
                        header.Offset + AssociationRequestFields.CapabilityInformationPosition);
                }

                set
                {
                    EndianBitConverter.Little.CopyBytes(value,
                        header.Bytes,
                        header.Offset + AssociationRequestFields.CapabilityInformationPosition);
                }
            }

            public CapabilityInformationField CapabilityInformation
            {
                get;
                set;
            }


            public UInt16 ListenInterval
            {
                get
                {
                    return EndianBitConverter.Little.ToUInt16(header.Bytes,
                        header.Offset + AssociationRequestFields.ListenIntervalPosition);
                }

                set
                {
                    EndianBitConverter.Little.CopyBytes(value,
                        header.Bytes,
                        header.Offset + AssociationRequestFields.ListenIntervalPosition);
                }
            }


            public InformationElementSection InformationElements { get; set; }

            public override int FrameSize
            {
                get
                {
                    return (MacFields.FrameControlLength +
                        MacFields.DurationIDLength +
                        (MacFields.AddressLength * 3) +
                        MacFields.SequenceControlLength +
                        AssociationRequestFields.CapabilityInformationLength +
                        AssociationRequestFields.ListenIntervalLength +
                        InformationElements.Length);
                }
            }


            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="bas">
            /// A <see cref="ByteArraySegment"/>
            /// </param>
            public AssociationRequestFrame(ByteArraySegment bas)
            {
                header = new ByteArraySegment(bas);

                FrameControl = new FrameControlField(FrameControlBytes);
                Duration = new DurationField(DurationBytes);
                SequenceControl = new SequenceControlField(SequenceControlBytes);

                CapabilityInformation = new CapabilityInformationField(CapabilityInformationBytes);

                //create a segment that just refers to the info element section
                ByteArraySegment infoElementsSegment = new ByteArraySegment(bas.Bytes,
                    (bas.Offset + AssociationRequestFields.InformationElement1Position),
                    (bas.Length - AssociationRequestFields.InformationElement1Position - MacFields.FrameCheckSequenceLength));

                InformationElements = new InformationElementSection(infoElementsSegment);

                //cant set length until after we have handled the information elements
                //as they vary in length
                header.Length = FrameSize;
            }

            /// <summary>
            /// ToString() override
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/>
            /// </returns>
            public override string ToString()
            {
                return string.Format("FrameControl {0}, FrameCheckSequence {1}, [AssociationRequestFrame RA {2} TA {3} BSSID {4}]",
                                     FrameControl.ToString(),
                                     FrameCheckSequence,
                                     DestinationAddress.ToString(),
                                     SourceAddress.ToString(),
                                     BssId.ToString());
            }
        } 
    }
}