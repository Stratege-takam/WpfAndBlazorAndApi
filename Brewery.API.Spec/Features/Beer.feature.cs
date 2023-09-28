﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Brewery.API.Spec.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class BeerFeature : object, Xunit.IClassFixture<BeerFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Beer.feature"
#line hidden
        
        public BeerFeature(BeerFeature.FixtureData fixtureData, Brewery_API_Spec_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "Beer", "\tBrewery beer management", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="1- Successful creation of a new beer")]
        [Xunit.TraitAttribute("FeatureTitle", "Beer")]
        [Xunit.TraitAttribute("Description", "1- Successful creation of a new beer")]
        public virtual void _1_SuccessfulCreationOfANewBeer()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("1- Successful creation of a new beer", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 5
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 6
 testRunner.Given("User fills in the name of the beer \"Leffe black\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 7
 testRunner.And("User enters the identifier of the beer brewer \"08d9c2d4-bd3b-4129-8153-4924870c9c" +
                        "b7\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 8
 testRunner.And("User enters the price of the beer 12.39", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 9
 testRunner.And("User enters the degree of the beer 34.88", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 10
 testRunner.When("User submit result", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 11
 testRunner.Then("The status code is Success", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 12
 testRunner.And("The degree must be \"34,88%\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 13
 testRunner.And("The identifiant must be not null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="2- Error creating a beer with a non-existent brewer")]
        [Xunit.TraitAttribute("FeatureTitle", "Beer")]
        [Xunit.TraitAttribute("Description", "2- Error creating a beer with a non-existent brewer")]
        [Xunit.InlineDataAttribute("Beer test 1", "08d9c2d4-bd3b-4120-8153-4924870c9cb7", "11.23", "21.10", new string[0])]
        [Xunit.InlineDataAttribute("Beer test 2", "08d9c2d4-bd3b-4100-8153-4924870c9cb7", "13.0", "1.00", new string[0])]
        [Xunit.InlineDataAttribute("Beer test 3", "08d9c2d4-bd3b-4000-8153-4924870c9cb7", "100", "12.00", new string[0])]
        [Xunit.InlineDataAttribute("Beer test 4", "08d9c2d4-bd3b-4002-8153-4924870c9cb7", "33.11", "2.25", new string[0])]
        [Xunit.InlineDataAttribute("Beer test 5", "08d9c2d4-bd3b-2002-8153-4924870c9cb7", "12.12", "21.15", new string[0])]
        public virtual void _2_ErrorCreatingABeerWithANon_ExistentBrewer(string name, string ownerId, string price, string degree, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("name", name);
            argumentsOfScenario.Add("ownerId", ownerId);
            argumentsOfScenario.Add("price", price);
            argumentsOfScenario.Add("degree", degree);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("2- Error creating a beer with a non-existent brewer", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 16
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 17
 testRunner.Given(string.Format("The user fills in the requested information with a non-existent brewer ({0},{1},{" +
                            "2},{3})", name, ownerId, price, degree), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 18
 testRunner.When("User submit the form", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 19
 testRunner.Then("The status code is BadParams", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 20
 testRunner.And("The reason is The identification of the introduced owner does not exist", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 21
 testRunner.And("The data is null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="3- Successful creation of many beer")]
        [Xunit.TraitAttribute("FeatureTitle", "Beer")]
        [Xunit.TraitAttribute("Description", "3- Successful creation of many beer")]
        [Xunit.InlineDataAttribute("Beer test 1", "08d9c2d4-bd3b-4129-8153-4924870c9cb7", "11.23", "21.10", new string[0])]
        [Xunit.InlineDataAttribute("Beer test 2", "08d9c2d4-bd3b-4129-8153-4924870c9cb7", "13.0", "1.00", new string[0])]
        [Xunit.InlineDataAttribute("Beer test 3", "08d9c2d4-bd3b-4129-8153-4924870c9cb7", "100", "12.00", new string[0])]
        [Xunit.InlineDataAttribute("Beer test 4", "08d9c2d4-bd3b-4129-8153-4924870c9cb8", "33.11", "2.25", new string[0])]
        [Xunit.InlineDataAttribute("Beer test 5", "08d9c2d4-bd3b-4129-8153-4924870c9cb8", "12.12", "21.15", new string[0])]
        public virtual void _3_SuccessfulCreationOfManyBeer(string name, string ownerId, string price, string degree, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("name", name);
            argumentsOfScenario.Add("ownerId", ownerId);
            argumentsOfScenario.Add("price", price);
            argumentsOfScenario.Add("degree", degree);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("3- Successful creation of many beer", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 32
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 33
 testRunner.Given(string.Format("The user fills in the all requested information ({0},{1},{2},{3})", name, ownerId, price, degree), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 34
 testRunner.When("The user submits his request and the system processes the information", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 35
 testRunner.Then("The all status code is Success", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 36
 testRunner.And("The system returns each registered beer to the user with ID", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="4- Error when creating an existing beer")]
        [Xunit.TraitAttribute("FeatureTitle", "Beer")]
        [Xunit.TraitAttribute("Description", "4- Error when creating an existing beer")]
        [Xunit.TraitAttribute("Category", "mytag")]
        [Xunit.InlineDataAttribute("Leffe Blonde", "08d9c2d4-bd3b-4129-8153-4924870c9cb7", "11.23", "21.10", new string[0])]
        [Xunit.InlineDataAttribute("Beer 2", "08d9c2d4-bd3b-4129-8153-4924870c9cb7", "13.0", "1.00", new string[0])]
        [Xunit.InlineDataAttribute("Beer 3", "08d9c2d4-bd3b-4129-8153-4924870c9cb7", "100", "12.00", new string[0])]
        [Xunit.InlineDataAttribute("Beer 4", "08d9c2d4-bd3b-4129-8153-4924870c9cb8", "33.11", "2.25", new string[0])]
        [Xunit.InlineDataAttribute("Beer 5", "08d9c2d4-bd3b-4129-8153-4924870c9cb8", "12.12", "21.15", new string[0])]
        public virtual void _4_ErrorWhenCreatingAnExistingBeer(string name, string ownerId, string price, string degree, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "mytag"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("name", name);
            argumentsOfScenario.Add("ownerId", ownerId);
            argumentsOfScenario.Add("price", price);
            argumentsOfScenario.Add("degree", degree);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("4- Error when creating an existing beer", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 49
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 50
 testRunner.Given(string.Format("The user fills the form ({0},{1},{2},{3})", name, ownerId, price, degree), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 51
 testRunner.When("He submit", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 52
 testRunner.Then("Every status code is BadParams", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 53
 testRunner.And("The reason is A beer already exists under this name. Beer name must be unique", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 54
 testRunner.And("The data is null because an existing beer", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                BeerFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                BeerFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion