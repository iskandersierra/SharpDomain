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
namespace SharpDomain.Specs.Messaging
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Envelope")]
    [NUnit.Framework.CategoryAttribute("infrastructure")]
    [NUnit.Framework.CategoryAttribute("messaging")]
    public partial class EnvelopeFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Envelope.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Envelope", "Specification for Envelope and Envelope<> class\r\nAn envelope represents a generic" +
                    " messager aware of the messaging mechanism, so besides\r\na generic body it can co" +
                    "ntain other properties like delay times, correlation ids, and others", ProgrammingLanguage.CSharp, new string[] {
                        "infrastructure",
                        "messaging"});
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
        [NUnit.Framework.DescriptionAttribute("Create an envelope with simple string body using Envelope.Create")]
        [NUnit.Framework.TestCaseAttribute("hello", null)]
        [NUnit.Framework.TestCaseAttribute("", null)]
        [NUnit.Framework.TestCaseAttribute("1", null)]
        public virtual void CreateAnEnvelopeWithSimpleStringBodyUsingEnvelope_Create(string message, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create an envelope with simple string body using Envelope.Create", exampleTags);
#line 8
this.ScenarioSetup(scenarioInfo);
#line 9
 testRunner.Given(string.Format("An envelope is created with simple string body \"{0}\" using Envelope.Create", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
 testRunner.Then("The envelope is not null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 11
 testRunner.And("The envelope type is Envelope of String", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And(string.Format("The envelope body is equals to string \"{0}\"", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And(string.Format("The envelope is implicitly equals to string \"{0}\"", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create an envelope with simple string body using new Envelope")]
        [NUnit.Framework.TestCaseAttribute("hello", null)]
        [NUnit.Framework.TestCaseAttribute("", null)]
        [NUnit.Framework.TestCaseAttribute("1", null)]
        public virtual void CreateAnEnvelopeWithSimpleStringBodyUsingNewEnvelope(string message, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create an envelope with simple string body using new Envelope", exampleTags);
#line 20
this.ScenarioSetup(scenarioInfo);
#line 21
 testRunner.Given(string.Format("An envelope is created with simple string body \"{0}\" using new Envelope", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 22
 testRunner.Then("The envelope is not null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 23
 testRunner.And("The envelope type is Envelope of String", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.And(string.Format("The envelope body is equals to string \"{0}\"", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.And(string.Format("The envelope is implicitly equals to string \"{0}\"", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create an envelope with simple string body using implicit operator")]
        [NUnit.Framework.TestCaseAttribute("hello", null)]
        [NUnit.Framework.TestCaseAttribute("", null)]
        [NUnit.Framework.TestCaseAttribute("1", null)]
        public virtual void CreateAnEnvelopeWithSimpleStringBodyUsingImplicitOperator(string message, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create an envelope with simple string body using implicit operator", exampleTags);
#line 32
this.ScenarioSetup(scenarioInfo);
#line 33
 testRunner.Given(string.Format("An envelope is created with simple string body \"{0}\" using implicit operator", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 34
 testRunner.Then("The envelope is not null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 35
 testRunner.And("The envelope type is Envelope of String", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.And(string.Format("The envelope body is equals to string \"{0}\"", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
 testRunner.And(string.Format("The envelope is implicitly equals to string \"{0}\"", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion