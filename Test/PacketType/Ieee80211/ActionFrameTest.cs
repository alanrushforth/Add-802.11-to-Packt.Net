﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpPcap.LibPcap;
using PacketDotNet;
using PacketDotNet.Utils;
using PacketDotNet.Ieee80211;

namespace Test.PacketType
{
    namespace Ieee80211
    {
        [TestFixture]
        class ActionFrameTest
        {
            /// <summary>
            /// Test that parsing a add block ack response report frame yields the proper field values
            /// </summary>
            [Test]
            public void Test_Constructor_AddBlockAckResponseReport()
            {
                var dev = new CaptureFileReaderDevice("../../CaptureFiles/80211_block_ack_response_action_frame.pcap");
                dev.Open();
                var rawCapture = dev.GetNextPacket();
                dev.Close();

                Packet p = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
                ActionFrame frame = (ActionFrame)p.PayloadPacket;

                Assert.AreEqual(0, frame.FrameControl.ProtocolVersion);
                Assert.AreEqual(FrameControlField.FrameTypes.ManagementAction, frame.FrameControl.Type);
                Assert.IsFalse(frame.FrameControl.ToDS);
                Assert.IsFalse(frame.FrameControl.FromDS);
                Assert.IsFalse(frame.FrameControl.MoreFragments);
                Assert.IsFalse(frame.FrameControl.Retry);
                Assert.IsFalse(frame.FrameControl.PowerManagement);
                Assert.IsFalse(frame.FrameControl.MoreData);
                Assert.IsFalse(frame.FrameControl.Wep);
                Assert.IsFalse(frame.FrameControl.Order);
                Assert.AreEqual(314, frame.Duration.Field); //this need expanding on in the future
                Assert.AreEqual("0024B2F8D706", frame.DestinationAddress.ToString().ToUpper());
                Assert.AreEqual("7CC5376D16E7", frame.SourceAddress.ToString().ToUpper());
                Assert.AreEqual("0024B2F8D706", frame.BssId.ToString().ToUpper());
                Assert.AreEqual(0, frame.SequenceControl.FragmentNumber);
                Assert.AreEqual(3826, frame.SequenceControl.SequenceNumber);

                Assert.AreEqual(0x6D3FCFA3, frame.FrameCheckSequence);
                Assert.AreEqual(24, frame.FrameSize);
                Assert.AreEqual(9, frame.PayloadData.Length);
            }
        } 
    }
}