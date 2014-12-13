﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.34014
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace ContactsContext.EventSourcing.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("ContactCommandHandlers")]
    [NUnit.Framework.CategoryAttribute("domain")]
    [NUnit.Framework.CategoryAttribute("contacts")]
    public partial class ContactCommandHandlersFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ContactCommandHandlers.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ContactCommandHandlers", "Contact command handlers specifications", ProgrammingLanguage.CSharp, new string[] {
                        "domain",
                        "contacts"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A create contact command raises a contact created and a contact title updated eve" +
            "nts")]
        [NUnit.Framework.TestCaseAttribute("{6010B03D-B110-42CA-87B1-0C6B926C6E4E}", "Iskander", "2", null)]
        [NUnit.Framework.TestCaseAttribute("{4573A8A5-6E33-4BFA-8F5F-4639AAF44A23}", "", "2", null)]
        public virtual void ACreateContactCommandRaisesAContactCreatedAndAContactTitleUpdatedEvents(string contactId, string title, string eventCount, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A create contact command raises a contact created and a contact title updated eve" +
                    "nts", exampleTags);
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given("a create contact command handler", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
 testRunner.Given("a command handler context", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And(string.Format("a create contact command is created with \"{0}\" and \"{1}\"", contactId, title), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.When("the create contact command handler handles the command", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.Then(string.Format("the command handler context has {0} emmitted events", eventCount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 12
 testRunner.And(string.Format("the command handler context has a contact created event as event 1 with \"{0}\"", contactId), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And(string.Format("the command handler context has a contact title updated event as event 2 with \"{0" +
                        "}\" and \"{1}\"", contactId, title), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A update contact title command raises a contact title updated event")]
        [NUnit.Framework.TestCaseAttribute("{6010B03D-B110-42CA-87B1-0C6B926C6E4E}", "Hello", "1", null)]
        public virtual void AUpdateContactTitleCommandRaisesAContactTitleUpdatedEvent(string contactId, string title, string eventCount, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A update contact title command raises a contact title updated event", exampleTags);
#line 19
this.ScenarioSetup(scenarioInfo);
#line 20
 testRunner.Given("a update contact title command handler", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
 testRunner.And("a command handler context", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.And(string.Format("a update contact title command is created with \"{0}\" and \"{1}\"", contactId, title), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.When("the update contact title command handler handles the command", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 24
 testRunner.Then(string.Format("the command handler context has {0} emmitted events", eventCount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 25
 testRunner.And(string.Format("the command handler context has a contact title updated event as event 1 with \"{0" +
                        "}\" and \"{1}\"", contactId, title), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A update contact picture command raises a contact picture updated event")]
        [NUnit.Framework.TestCaseAttribute("{6010B03D-B110-42CA-87B1-0C6B926C6E4E}", "{1811C884-0030-4F06-8FC0-2E6DCD28FD77}", "1", null)]
        public virtual void AUpdateContactPictureCommandRaisesAContactPictureUpdatedEvent(string contactId, string pictureId, string eventCount, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A update contact picture command raises a contact picture updated event", exampleTags);
#line 30
this.ScenarioSetup(scenarioInfo);
#line 31
 testRunner.Given("a update contact picture command handler", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 32
 testRunner.And("a command handler context", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And(string.Format("a update contact picture command is created with \"{0}\" and \"{1}\"", contactId, pictureId), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.When("the update contact picture command handler handles the command", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
 testRunner.Then(string.Format("the command handler context has {0} emmitted events", eventCount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 36
 testRunner.And(string.Format("the command handler context has a contact picture updated event as event 1 with \"" +
                        "{0}\" and \"{1}\"", contactId, pictureId), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A clear contact picture command raises a contact picture cleared event")]
        [NUnit.Framework.TestCaseAttribute("{6010B03D-B110-42CA-87B1-0C6B926C6E4E}", "1", null)]
        public virtual void AClearContactPictureCommandRaisesAContactPictureClearedEvent(string contactId, string eventCount, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A clear contact picture command raises a contact picture cleared event", exampleTags);
#line 41
this.ScenarioSetup(scenarioInfo);
#line 42
 testRunner.Given("a clear contact picture command handler", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 43
 testRunner.And("a command handler context", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And(string.Format("a clear contact picture command is created with \"{0}\"", contactId), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.When("the clear contact picture command handler handles the command", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 46
 testRunner.Then(string.Format("the command handler context has {0} emmitted events", eventCount), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 47
 testRunner.And(string.Format("the command handler context has a contact picture cleared event as event 1 with \"" +
                        "{0}\"", contactId), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
