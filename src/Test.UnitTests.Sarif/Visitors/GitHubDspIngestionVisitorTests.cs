﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.CodeAnalysis.Sarif.Visitors
{
    public class GitHubDspIngestionVisitorTests : FileDiffingUnitTests
    {
        public GitHubDspIngestionVisitorTests(ITestOutputHelper outputHelper) : base(outputHelper) { }

        protected override string ConstructTestOutputFromInputResource(string inputResourceName, object parameter)
        {
            string logContents = GetResourceText(inputResourceName);
            SarifLog sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var visitor = new GitHubDspIngestionVisitor();
            visitor.Visit(sarifLog);

            return JsonConvert.SerializeObject(sarifLog, SarifTransformerUtilities.JsonSettingsIndented);
        }

        [Fact]
        public void GitHubDspIngestionVisitor_FiltersNonErrorResults()
            => RunTest("NonErrorResults.sarif");
    }
}
