using System;
using System.IO;
using System.Security;
using FluentAssertions;
using nucs.JsonSettings.Fluent;
using nucs.JsonSettings.Modulation;
using nucs.JsonSettings.xTests.Utils;
using Xunit;
using Xunit.Sdk;

namespace nucs.JsonSettings.xTests {
    public class HowToUse {
        #region Settings Bag

        [Fact]
        public void Use_MostBasic() {
            using (var f = new TempfileLife()) {
                //used for autodelete file after test ends
                var o = JsonSettings.Load<CasualExampleSettings>(f.FileName);
                o.SomeNumeralProperty = 1;
                o.SomeProperty = "with some value";
                o.SomeClassProperty = new SmallClass() {Name = "Small", Value = "Class"};
                o.Save();


                //validate
                o = JsonSettings.Load<CasualExampleSettings>(f.FileName);
                o.SomeProperty.Should().Be("with some value");
                o.SomeNumeralProperty.ShouldBeEquivalentTo(1);
                o.SomeClassProperty.Should().BeOfType(typeof(SmallClass)).And.Match(obj => (obj as SmallClass).Name == "Small");
            }
        }

        [Fact]
        public void Use_Configure_CasualSettingsExample() {
            using (var f = new TempfileLife()) {
                //load using Configure
                var o = JsonSettings.Configure<CasualExampleSettings>(f.FileName).WithBase64().WithEncryption("SuperPassword").LoadNow();
                o.SomeNumeralProperty = 1;
                o.SomeProperty = "with some value";
                o.SomeClassProperty = new SmallClass() {Name = "Small", Value = "Class"};
                o.Save();

                //validate
                o = JsonSettings.Configure<CasualExampleSettings>(f.FileName).WithBase64().WithEncryption("SuperPassword").LoadNow();
                o.SomeProperty.Should().Be("with some value");
                o.SomeNumeralProperty.ShouldBeEquivalentTo(1);
                o.SomeClassProperty.Should().BeOfType(typeof(SmallClass)).And.Match(obj => (obj as SmallClass).Name == "Small");
            }
        }
        [Fact]
        public void Use_Configure_CasualSettingsExample_Load() {
            using (var f = new TempfileLife()) {
                //load using Load      with configuration
                var o = JsonSettings.Load<CasualExampleSettings>(f.FileName, s => s.WithBase64().WithEncryption("SuperPassword"));
                o.SomeNumeralProperty = 1;
                o.SomeProperty = "with some value";
                o.SomeClassProperty = new SmallClass() {Name = "Small", Value = "Class"};
                o.Save();

                //validate
                o = JsonSettings.Load<CasualExampleSettings>(f.FileName, s => s.WithBase64().WithEncryption("SuperPassword"));
                o.SomeProperty.Should().Be("with some value");
                o.SomeNumeralProperty.ShouldBeEquivalentTo(1);
                o.SomeClassProperty.Should().BeOfType(typeof(SmallClass)).And.Match(obj => (obj as SmallClass).Name == "Small");
            }
        }

        [Fact]
        public void Use_Configure_CasualSettingsExample_LoadSelf() {
            using (var f = new TempfileLife()) {
                //load using Load      with configuration and custom constructor and password fetcher from itself.
                var o = JsonSettings.Load<CasualExampleSettings>(f.FileName, s => s.WithBase64().WithEncryption(set => set.SomeProperty), new object[] {"SuperPassword"});
                o.SomeNumeralProperty = 1;
                o.SomeClassProperty = new SmallClass() {Name = "Small", Value = "Class"};
                o.Save();

                //validate
                o = JsonSettings.Load<CasualExampleSettings>(f.FileName, s => s.WithBase64().WithEncryption(set => set.SomeProperty), new object[] {"SuperPassword"});
                o.SomeProperty.Should().Be("SuperPassword");
                o.SomeNumeralProperty.ShouldBeEquivalentTo(1);
                o.SomeClassProperty.Should().BeOfType(typeof(SmallClass)).And.Match(obj => (obj as SmallClass).Name == "Small");
            }
        }
        [Fact]
        public void Use_SettingsBag() {
            using (var f = new TempfileLife()) {
                //used for autodelete file after test ends
                var o = JsonSettings.Load<SettingsBag>(f.FileName);
                o["somekey"] = "with some value";
                o["someotherkey"] = 1;
                o["somekeyforclass"] = new SmallClass() {Name = "Small", Value = "Class"};
                o.Save();

                //validate
                o = JsonSettings.Load<SettingsBag>(f.FileName);
                o["somekey"].Should().Be("with some value");
                o["someotherkey"].ShouldBeEquivalentTo(1);
                o["somekeyforclass"].Should().BeOfType(typeof(SmallClass)).And.Match(obj => (obj as SmallClass).Name == "Small");
            }
        }

        [Fact]
        public void Use_SettingsBag_AutoSave() {
            using (var f = new TempfileLife()) {
                //used for autodelete file after test ends
                var o = JsonSettings.Load<SettingsBag>(f.FileName).EnableAutosave();
                o["somekey"] = "with some value";
                o["someotherkey"] = 1;
                o["somekeyforclass"] = new SmallClass() {Name = "Small", Value = "Class"};

                //validate
                o = JsonSettings.Load<SettingsBag>(f.FileName);
                o["somekey"].Should().Be("with some value");
                o["someotherkey"].ShouldBeEquivalentTo(1);
                o["somekeyforclass"].Should().BeOfType(typeof(SmallClass)).And.Match(obj => (obj as SmallClass).Name == "Small");
            }
        }

        /*
        [Fact]
        public void Use_EncryptedCasualSettingsExample() {
            using (var f = new TempfileLife()) {
                //used for autodelete file after test ends
                var o = EncryptedJsonSettings.Load<CasualExampleSettings>("password", f.FileName);
                o.SomeNumeralProperty = 1;
                o.SomeProperty = "with some value";
                o.SomeClassProperty = new SmallClass() {Name = "Small", Value = "Class"};
                o.Save();

                //validate
                o = EncryptedJsonSettings.Load<CasualExampleSettings>("password", f.FileName);
                o.SomeProperty.Should().Be("with some value");
                o.SomeNumeralProperty.ShouldBeEquivalentTo(1);
                o.SomeClassProperty.Should().BeOfType(typeof(SmallClass)).And.Match(obj => (obj as SmallClass).Name == "Small");
            }
        }*/

        [Fact]
        public void Use__CasualSettingsExample() {
            using (var f = new TempfileLife()) {
                //used for autodelete file after test ends
                var o = JsonSettings.Load<CasualExampleSettings>(f.FileName);
                o.SomeNumeralProperty = 1;
                o.SomeProperty = "with some value";
                o.SomeClassProperty = new SmallClass() {Name = "Small", Value = "Class"};
                o.Save();

                //validate
                o = JsonSettings.Load<CasualExampleSettings>(f.FileName);
                o.SomeProperty.Should().Be("with some value");
                o.SomeNumeralProperty.ShouldBeEquivalentTo(1);
                o.SomeClassProperty.Should().BeOfType(typeof(SmallClass)).And.Match(obj => (obj as SmallClass).Name == "Small");
            }
        }

        #endregion
    }
}