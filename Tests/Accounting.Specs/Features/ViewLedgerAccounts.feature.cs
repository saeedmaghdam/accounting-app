﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:1.0.0.0
//      Reqnroll Generator Version:1.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Accounting.Specs.Features
{
    using Reqnroll;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "1.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ViewLedgerAccountsFeature : object, Xunit.IClassFixture<ViewLedgerAccountsFeature.FixtureData>, Xunit.IAsyncLifetime
    {
        
        private static Reqnroll.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "ViewLedgerAccounts.feature"
#line hidden
        
        public ViewLedgerAccountsFeature(ViewLedgerAccountsFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }
        
        public static async System.Threading.Tasks.Task FeatureSetupAsync()
        {
            testRunner = Reqnroll.TestRunnerManager.GetTestRunnerForAssembly(null, Reqnroll.xUnit.ReqnrollPlugin.XUnitParallelWorkerTracker.Instance.GetWorkerId());
            Reqnroll.FeatureInfo featureInfo = new Reqnroll.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "View Ledger Accounts", "A short summary of the feature", ProgrammingLanguage.CSharp, featureTags);
            await testRunner.OnFeatureStartAsync(featureInfo);
        }
        
        public static async System.Threading.Tasks.Task FeatureTearDownAsync()
        {
            string testWorkerId = testRunner.TestWorkerId;
            await testRunner.OnFeatureEndAsync();
            testRunner = null;
            Reqnroll.xUnit.ReqnrollPlugin.XUnitParallelWorkerTracker.Instance.ReleaseWorker(testWorkerId);
        }
        
        public async System.Threading.Tasks.Task TestInitializeAsync()
        {
        }
        
        public async System.Threading.Tasks.Task TestTearDownAsync()
        {
            await testRunner.OnScenarioEndAsync();
        }
        
        public void ScenarioInitialize(Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public async System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
        {
            await this.TestInitializeAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
        {
            await this.TestTearDownAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Viewing all ledger accounts")]
        [Xunit.TraitAttribute("FeatureTitle", "View Ledger Accounts")]
        [Xunit.TraitAttribute("Description", "Viewing all ledger accounts")]
        [Xunit.TraitAttribute("Category", "tag1")]
        public async System.Threading.Tasks.Task ViewingAllLedgerAccounts()
        {
            string[] tagsOfScenario = new string[] {
                    "tag1"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            Reqnroll.ScenarioInfo scenarioInfo = new Reqnroll.ScenarioInfo("Viewing all ledger accounts", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 6
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
                Reqnroll.Table table14 = new Reqnroll.Table(new string[] {
                            "date",
                            "description",
                            "debit account",
                            "credit account",
                            "amount"});
                table14.AddRow(new string[] {
                            "2024-01-01",
                            "Investment by Owner",
                            "Cash",
                            "Owner\'s Equity",
                            "1000"});
                table14.AddRow(new string[] {
                            "2024-01-02",
                            "Purchase office supplies",
                            "Office Supplies",
                            "Accounts Payable",
                            "200"});
#line 7
    await testRunner.GivenAsync("multiple journal entries recorded", ((string)(null)), table14, "Given ");
#line hidden
#line 11
    await testRunner.WhenAsync("I view the ledger", ((string)(null)), ((Reqnroll.Table)(null)), "When ");
#line hidden
                Reqnroll.Table table15 = new Reqnroll.Table(new string[] {
                            "account",
                            "debit",
                            "credit",
                            "balance"});
                table15.AddRow(new string[] {
                            "Cash",
                            "1000",
                            "0",
                            "1000"});
                table15.AddRow(new string[] {
                            "Owner\'s Equity",
                            "0",
                            "1000",
                            "1000"});
                table15.AddRow(new string[] {
                            "Office Supplies",
                            "200",
                            "0",
                            "200"});
                table15.AddRow(new string[] {
                            "Accounts Payable",
                            "0",
                            "200",
                            "200"});
#line 12
    await testRunner.ThenAsync("I should see the updated balances of the ledger accounts", ((string)(null)), table15, "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "1.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : object, Xunit.IAsyncLifetime
        {
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
            {
                await ViewLedgerAccountsFeature.FeatureSetupAsync();
            }
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
            {
                await ViewLedgerAccountsFeature.FeatureTearDownAsync();
            }
        }
    }
}
#pragma warning restore
#endregion
