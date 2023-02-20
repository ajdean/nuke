// Copyright 2021 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.AzurePipelines.Configuration
{
    [PublicAPI]
    public class AzurePipelinesDownloadStep : AzurePipelinesStep
    {
        public string ArtifactName { get; set; }
        public string[] ItemPatterns { get; set; }
        
        public string TargetPath { get; set; }

        public override void Write(CustomFileWriter writer)
        {
            using (writer.WriteBlock("- task: DownloadPipelineArtifact@2"))
            {
                // writer.WriteLine("displayName: Download Artifacts");
                using (writer.WriteBlock("inputs:"))
                {
                    writer.WriteLine("buildType: 'current'");
                    writer.WriteLine($"artifactName: {ArtifactName.SingleQuote()}");
                    using (writer.WriteBlock("itemPattern: |"))
                    {
                        foreach (var itemPattern in ItemPatterns)
                        {
                            writer.WriteLine(itemPattern);
                        }
                    }
                    writer.WriteLine($"targetPath: '$(Build.Repository.LocalPath)/{TargetPath}'");
                }
            }
        }
    }
}
