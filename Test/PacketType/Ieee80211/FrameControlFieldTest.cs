﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PacketDotNet;
using PacketDotNet.Ieee80211;

namespace Test.PacketType
{
    namespace Ieee80211
    {
        [TestFixture]
        class FrameControlFieldTest
        {
            [Test]
            public void Test_Constructor_EncryptedDataFrame()
            {
                FrameControlField frameControl = new FrameControlField(0x0842);

                Assert.AreEqual(0x0, frameControl.ProtocolVersion);
                Assert.AreEqual(FrameControlField.FrameTypes.Data, frameControl.Type);
                Assert.IsTrue(frameControl.FromDS);
                Assert.IsTrue(frameControl.Wep);

                Assert.IsFalse(frameControl.ToDS);
                Assert.IsFalse(frameControl.MoreFragments);
                Assert.IsFalse(frameControl.Retry);
                Assert.IsFalse(frameControl.PowerManagement);
                Assert.IsFalse(frameControl.MoreData);
                Assert.IsFalse(frameControl.Order);
            }

            [Test]
            public void Test_Constructor_BeaconFrame()
            {
                FrameControlField frameControl = new FrameControlField(0x8000);

                Assert.AreEqual(0x0, frameControl.ProtocolVersion);
                Assert.AreEqual(FrameControlField.FrameTypes.ManagementBeacon, frameControl.Type);
                Assert.IsFalse(frameControl.FromDS);
                Assert.IsFalse(frameControl.Wep);
                Assert.IsFalse(frameControl.ToDS);
                Assert.IsFalse(frameControl.MoreFragments);
                Assert.IsFalse(frameControl.Retry);
                Assert.IsFalse(frameControl.PowerManagement);
                Assert.IsFalse(frameControl.MoreData);
                Assert.IsFalse(frameControl.Order);
            }

            [Test]
            public void Test_Constructor_ClearToSendFrame()
            {
                FrameControlField frameControl = new FrameControlField(0xC400);

                Assert.AreEqual(0x0, frameControl.ProtocolVersion);
                Assert.AreEqual(FrameControlField.FrameTypes.ControlCTS, frameControl.Type);
                Assert.IsFalse(frameControl.FromDS);
                Assert.IsFalse(frameControl.Wep);
                Assert.IsFalse(frameControl.ToDS);
                Assert.IsFalse(frameControl.MoreFragments);
                Assert.IsFalse(frameControl.Retry);
                Assert.IsFalse(frameControl.PowerManagement);
                Assert.IsFalse(frameControl.MoreData);
                Assert.IsFalse(frameControl.Order);
            }

            [Test]
            public void Test_Constructor_AckFrame()
            {
                FrameControlField frameControl = new FrameControlField(0xD410);

                Assert.AreEqual(0x0, frameControl.ProtocolVersion);
                Assert.AreEqual(FrameControlField.FrameTypes.ControlACK, frameControl.Type);
                Assert.IsFalse(frameControl.FromDS);
                Assert.IsFalse(frameControl.Wep);
                Assert.IsFalse(frameControl.ToDS);
                Assert.IsFalse(frameControl.MoreFragments);
                Assert.IsFalse(frameControl.Retry);
                Assert.IsTrue(frameControl.PowerManagement);
                Assert.IsFalse(frameControl.MoreData);
                Assert.IsFalse(frameControl.Order);
            }

            [Test]
            public void Test_Constructor_DisassociationFrame()
            {
                FrameControlField frameControl = new FrameControlField(0xA01B);

                Assert.AreEqual(0x0, frameControl.ProtocolVersion);
                Assert.AreEqual(FrameControlField.FrameTypes.ManagementDisassociation, frameControl.Type);
                Assert.IsTrue(frameControl.FromDS);
                Assert.IsTrue(frameControl.ToDS);
                Assert.IsFalse(frameControl.MoreFragments);
                Assert.IsFalse(frameControl.MoreData);
                Assert.IsFalse(frameControl.Wep);
                Assert.IsTrue(frameControl.Retry);
                Assert.IsTrue(frameControl.PowerManagement);
                Assert.IsFalse(frameControl.Order);
            }

            [Test]
            public void Test_Constructor_DataFrame()
            {
                FrameControlField frameControl = new FrameControlField(0x0801);

                Assert.AreEqual(0x0, frameControl.ProtocolVersion);
                Assert.AreEqual(FrameControlField.FrameTypes.Data, frameControl.Type);

                Assert.IsTrue(frameControl.ToDS);
                Assert.IsFalse(frameControl.FromDS);
                Assert.IsFalse(frameControl.MoreFragments);
                Assert.IsFalse(frameControl.Retry);
                Assert.IsFalse(frameControl.PowerManagement);
                Assert.IsFalse(frameControl.MoreData);
                Assert.IsFalse(frameControl.Wep);
                Assert.IsFalse(frameControl.Order);
            }

            [Test]
            public void Test_SetTypeProperty()
            {
                FrameControlField frameControl = new FrameControlField();

                frameControl.Type = FrameControlField.FrameTypes.Data;
                Assert.AreEqual(FrameControlField.FrameTypes.Data, frameControl.Type);

                frameControl.Type = FrameControlField.FrameTypes.ManagementAuthentication;
                Assert.AreEqual(FrameControlField.FrameTypes.ManagementAuthentication, frameControl.Type);

                frameControl.Type = FrameControlField.FrameTypes.ControlACK;
                Assert.AreEqual(FrameControlField.FrameTypes.ControlACK, frameControl.Type);
            }

            [Test]
            public void Test_SetProtocolVersionProperty()
            {
                FrameControlField frameControl = new FrameControlField();

                frameControl.ProtocolVersion = 3;
                Assert.AreEqual(3, frameControl.ProtocolVersion);

                frameControl.ProtocolVersion = 2;
                Assert.AreEqual(2, frameControl.ProtocolVersion);

                frameControl.ProtocolVersion = 1;
                Assert.AreEqual(1, frameControl.ProtocolVersion);

                frameControl.ProtocolVersion = 0;
                Assert.AreEqual(0, frameControl.ProtocolVersion);
            }

            [Test]
            [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Invalid protocol version value. Value must be in the range 0-3.")]
            public void Test_SetProtocolVersionProperty_ValueTooLarge()
            {
                FrameControlField frameControl = new FrameControlField();

                frameControl.ProtocolVersion = 4;
            }

            [Test]
            public void Test_SetToDsProperty()
            {
                FrameControlField frameControl = new FrameControlField();
                frameControl.ToDS = true;

                Assert.IsTrue(frameControl.ToDS);
            }

            [Test]
            public void Test_SetFromDsProperty()
            {
                FrameControlField frameControl = new FrameControlField();
                frameControl.FromDS = true;

                Assert.IsTrue(frameControl.FromDS);
            }

            [Test]
            public void Test_SetMoreFragmentsProperty()
            {
                FrameControlField frameControl = new FrameControlField();
                frameControl.MoreFragments = true;

                Assert.IsTrue(frameControl.MoreFragments);
            }

            [Test]
            public void Test_SetRetryProperty()
            {
                FrameControlField frameControl = new FrameControlField();
                frameControl.Retry = true;

                Assert.IsTrue(frameControl.Retry);
            }

            [Test]
            public void Test_SetPowerManagementProperty()
            {
                FrameControlField frameControl = new FrameControlField();
                frameControl.PowerManagement = true;

                Assert.IsTrue(frameControl.PowerManagement);
            }

            [Test]
            public void Test_SetMoreDataProperty()
            {
                FrameControlField frameControl = new FrameControlField();
                frameControl.MoreData = true;

                Assert.IsTrue(frameControl.MoreData);
            }

            [Test]
            public void Test_SetWepProperty()
            {
                FrameControlField frameControl = new FrameControlField();
                frameControl.Wep = true;

                Assert.IsTrue(frameControl.Wep);
            }

            [Test]
            public void Test_SetOrderProperty()
            {
                FrameControlField frameControl = new FrameControlField();
                frameControl.Order = true;

                Assert.IsTrue(frameControl.Order);
            }
        } 
    }
}